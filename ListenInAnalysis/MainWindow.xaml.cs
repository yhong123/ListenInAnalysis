using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ListenInAnalysis.Classes;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using Microsoft.Win32;


namespace ListenInAnalysis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string m_strDb = "listenin_rct";
        CDataset m_dataset = new CDataset();
        CUser m_user = new CUser();

        List<CheckBox> m_lsCheckBoxLC = new List<CheckBox>();

        //----------------------------------------------------------------------------------------------------
        // MainWindow
        //----------------------------------------------------------------------------------------------------
        public MainWindow()
        {
            InitializeComponent();

            lbProgress.Content = "";

            m_lsCheckBoxLC.Add(cb1); m_lsCheckBoxLC.Add(cb2); m_lsCheckBoxLC.Add(cb3); m_lsCheckBoxLC.Add(cb4); m_lsCheckBoxLC.Add(cb5);
            m_lsCheckBoxLC.Add(cb6); m_lsCheckBoxLC.Add(cb7); m_lsCheckBoxLC.Add(cb8); m_lsCheckBoxLC.Add(cb9); m_lsCheckBoxLC.Add(cb10);
            m_lsCheckBoxLC.Add(cb11); m_lsCheckBoxLC.Add(cb12); m_lsCheckBoxLC.Add(cb13); m_lsCheckBoxLC.Add(cb14); m_lsCheckBoxLC.Add(cb15);
            m_lsCheckBoxLC.Add(cb16); m_lsCheckBoxLC.Add(cb17); m_lsCheckBoxLC.Add(cb18); m_lsCheckBoxLC.Add(cb19); m_lsCheckBoxLC.Add(cb20);
            m_lsCheckBoxLC.Add(cb21); m_lsCheckBoxLC.Add(cb22); m_lsCheckBoxLC.Add(cb23); m_lsCheckBoxLC.Add(cb24); m_lsCheckBoxLC.Add(cb25);
            m_lsCheckBoxLC.Add(cb26); m_lsCheckBoxLC.Add(cb27); m_lsCheckBoxLC.Add(cb28); m_lsCheckBoxLC.Add(cb29); m_lsCheckBoxLC.Add(cb30);

            int intLCNum = Enum.GetNames(typeof(CConstants.g_LinguisticCategory)).Length;
            for (int i = 0; i < intLCNum; i++)
            {
               // m_lsCheckBoxLC[i].Content = Enum.GetNames(typeof(CConstants.g_LinguisticCategory))[i].ToString();
            }

            // load user list from database and populate list box      
            //if (m_bRealtime)
            {
                string strPatientList = databaseGetPatientList();
                string[] line_r = strPatientList.Split(',');
                int intNumCols = line_r.Length;
                for (int j = 0; j < intNumCols - 1; j++)
                {
                    lbUser.Items.Add(line_r[j]);
                    //Console.WriteLine(line_r[j]);                
                }
            }           
            
            lbUser.SelectedIndex = 0;
        }

        //----------------------------------------------------------------------------------------------------
        // lbUser_SelectionChanged
        //----------------------------------------------------------------------------------------------------
        private void lbUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbProgress.Content = "Downloading data, please wait...";

            // uncheck all linguitic category
            for (int i = 0; i < m_lsCheckBoxLC.Count; i++)
                m_lsCheckBoxLC[i].IsChecked = false;                    

            string strUserId = "";
            ListBox lb = (ListBox)sender;
            strUserId = (string)lb.Items[lb.SelectedIndex];

            // load dataset
            //if (strUserId.Equals("3"))
            //    m_dataset.loadDataset(1);
            //else 
            //    m_dataset.loadDataset(getUserDatasetId(strUserId));

            m_user.clearBindList();

            // load from online db
            List<string> lsGameDate = new List<string>();
            List<double> lsGameTimeMin = new List<double>();
            //if (m_bRealtime)
            {
                string strUserProfile = databaseGetPatientData("http://italk.ucl.ac.uk/" + m_strDb + "/patient_userprofile_select.php", strUserId);
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("data/user_" + strUserId + "_profile.xml"))
                {
                    sw.WriteLine(strUserProfile);
                }
                string strTherapyBlocks = databaseGetPatientData("http://italk.ucl.ac.uk/" + m_strDb + "/patient_therapyblocks_select.php", strUserId);
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("data/user_" + strUserId + "_therapyblocks.xml"))
                {
                    sw.WriteLine(strTherapyBlocks);
                }
                string strTherapyBlocks_csv = databaseGetPatientData("http://italk.ucl.ac.uk/" + m_strDb + "/patient_therapyblocks_csv_select.php", strUserId);
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("data/user_" + strUserId + "_therapyblocks.csv"))
                {
                    sw.WriteLine(strTherapyBlocks_csv);
                }
                string strCifeaturesHistory = databaseGetPatientData("http://italk.ucl.ac.uk/" + m_strDb + "/patient_cifeatures_select.php", strUserId);
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("data/user_" + strUserId + "_challengeitemfeatures_history.xml"))
                {
                    sw.WriteLine(strCifeaturesHistory);
                }
                string strTherapyBlocksDetail_csv = databaseGetPatientData("http://italk.ucl.ac.uk/" + m_strDb + "/patient_therapyblocks_detail_csv_select.php", strUserId);
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("data/user_" + strUserId + "_therapyblocks_all.csv"))
                {
                    sw.WriteLine(strTherapyBlocksDetail_csv);
                }

                string strXml = databaseGetPatientData("http://italk.ucl.ac.uk/" + m_strDb + "/patient_therapytime_select.php", strUserId);
                XElement root = XElement.Parse(strXml);
                Console.WriteLine("xml = " + strXml);

                // load dataset
                m_dataset.loadDataset(strUserId, getUserDatasetId(strUserId));

                // therapy time
                string strTherapyTimeMin = (string)root.Element("therapyTime");
                //Console.WriteLine("therapy_time = " + strTherapyTime);

                // game time                
                lsGameDate = (
                       from el in root.Elements("game_time").Elements("gt")
                       select ((string)el.Attribute("date"))
                   ).ToList();
                lsGameTimeMin = (
                       from el in root.Elements("game_time").Elements("gt")
                       select ((double)el.Attribute("min"))
                   ).ToList();

                // load user
                m_user.loadProfile("data/", strUserId, m_dataset.getLexicalItemList(), m_dataset.getChallengeItemList(), m_dataset.getChallengeItemFeaturesList());
            }

            lbProgress.Content = "";

            /*else
            {
                // load dataset
                m_dataset.loadDataset(strUserId, getUserDatasetId(strUserId));
                // load offline files
                // load user
                m_user.loadProfile("data/", strUserId, m_dataset.getLexicalItemList(), m_dataset.getChallengeItemList(), m_dataset.getChallengeItemFeaturesList());
            }*/

            /*if (dgBlock != null)
            {
                dgBlock.ItemsSource = m_user.getBindBlockList();
                dgBlock.DataContext = m_user.getBindBlockList();
                dgBlock.Items.Refresh();

                dgLexicalItem.ItemsSource = m_user.getBindLexicalItemList();
                dgLexicalItem.DataContext = m_user.getBindLexicalItemList();
                dgLexicalItem.Items.Refresh();
                double dPercentage = Math.Round((double)(m_user.getLexicalItemCoverage() / (double)m_dataset.getLexicalItemList().Count * 100), 2);
                lbLexicalItemCoverage.Content = "Lexical items: coverage = " + m_user.getLexicalItemCoverage() + " items " + " (" + dPercentage + "%), accuracy = " + m_user.getLexicalItemAccuracy();

                dgChallengeItem.ItemsSource = m_user.getBindChallengeItemList();
                dgChallengeItem.DataContext = m_user.getBindChallengeItemList();
                dgChallengeItem.Items.Refresh();
                dPercentage = Math.Round((double)(m_user.getChallengeItemCoverage() / (double)m_dataset.getChallengeItemList().Count * 100), 2);
                lbChallengeItemCoverage.Content = "Challenge items: coverage = " + m_user.getChallengeItemCoverage() + " items " + " (" + dPercentage + "%), accuracy = " + m_user.getChallengeItemAccuracy();

                dgChallengeItemFeatures.ItemsSource = m_user.getBindChallengeItemFeaturesList();
                dgChallengeItemFeatures.DataContext = m_user.getBindChallengeItemFeaturesList();
                dgChallengeItemFeatures.Items.Refresh();
                dPercentage = Math.Round((double)(m_user.getChallengeItemFeaturesCoverage() / (double)m_dataset.getChallengeItemFeaturesList().Count * 100), 2);
                lbChallengeItemFeaturesCoverage.Content = "Challenge item features: coverage = " + m_user.getChallengeItemFeaturesCoverage() + " items " + " (" + dPercentage + "%), accuracy = " + m_user.getChallengeItemFeaturesAccuracy();

                dgForcedItem.ItemsSource = m_user.getBindForcedItemList();
                dgForcedItem.DataContext = m_user.getBindForcedItemList();
                dgForcedItem.Items.Refresh();               

                bindDailyTherapyTime(strUserId, lsGameDate, lsGameTimeMin);
            }*/
        }

        //----------------------------------------------------------------------------------------------------
        // databaseGetPatientList
        //----------------------------------------------------------------------------------------------------
        private string databaseGetPatientList()
        {
            Uri address = new Uri("http://italk.ucl.ac.uk/" + m_strDb + "/get_patients.php");

            // Create the web request  
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

            // Set type to POST  
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            StringBuilder data = new StringBuilder();
            //data.Append("patient=" + strUserId);
            //data.Append("appid=" + HttpUtility.UrlEncode(appId));
            //data.Append("&context=" + HttpUtility.UrlEncode(context));

            // Create a byte array of the data we want to send  
            byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());

            // Set the content length in the request headers  
            request.ContentLength = byteData.Length;

            // Write data  
            using (Stream postStream = request.GetRequestStream())
            {
                postStream.Write(byteData, 0, byteData.Length);
            }

            // Get response  
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream  
                StreamReader reader = new StreamReader(response.GetResponseStream());

                // Console application output  
                //Console.WriteLine("Patient list = " + reader.ReadToEnd());

                return reader.ReadToEnd();
            }
        }

        //----------------------------------------------------------------------------------------------------
        // databaseGetPatientData
        //----------------------------------------------------------------------------------------------------
        private string databaseGetPatientData(string strUri, string strUserId)
        {
            //Uri address = new Uri("http://italk.ucl.ac.uk/" + m_strDb + "/patient_data_select.php");
            Uri address = new Uri(strUri);

            // Create the web request  
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

            // Set type to POST  
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            StringBuilder data = new StringBuilder();
            data.Append("patient=" + strUserId);
            //data.Append("appid=" + HttpUtility.UrlEncode(appId));
            //data.Append("&context=" + HttpUtility.UrlEncode(context));

            // Create a byte array of the data we want to send  
            byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());

            // Set the content length in the request headers  
            request.ContentLength = byteData.Length;

            // Write data  
            using (Stream postStream = request.GetRequestStream())
            {
                postStream.Write(byteData, 0, byteData.Length);
            }

            // Get response  
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream  
                StreamReader reader = new StreamReader(response.GetResponseStream());

                // Console application output  
                //Console.WriteLine("\n\n\n\n\n" + reader.ReadToEnd());

                return reader.ReadToEnd();
            }
        }

        //----------------------------------------------------------------------------------------------------
        // getUserDatasetId (xml)
        //----------------------------------------------------------------------------------------------------
        private int getUserDatasetId(string strUserId)
        {
            // check if file exists
            string strXmlFile = "data/" + "user_" + strUserId + "_profile.xml";
            if (!System.IO.File.Exists(strXmlFile))
                return 0;

            XElement root = XElement.Load(strXmlFile);
            int intDatasetId = (int)root.Element("datasetId");
            return intDatasetId;
        }

        //----------------------------------------------------------------------------------------------------
        // btnItemLevelExport_Click
        //----------------------------------------------------------------------------------------------------
        private void btnItemLevelExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "unknown.csv";
            //saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                //File.WriteAllText(saveFileDialog.FileName, txtEditor.Text);
                //using (StreamWriter sw = new StreamWriter(savefile.FileName))
                //    sw.WriteLine("Hello World!");
                
                //Console.WriteLine(saveFileDialog.FileName);

                m_user.exportItemlevelAnalysis_Csv(saveFileDialog.FileName);
            }                       
        }

        //----------------------------------------------------------------------------------------------------
        // btnLinguisticCategoryExport_Click
        //----------------------------------------------------------------------------------------------------
        private void btnLinguisticCategoryExport_Click(object sender, RoutedEventArgs e)
        {           
            List<int> lsIdx = new List<int>();
            for (int i = 0; i < m_lsCheckBoxLC.Count; i++)
            {
                if (m_lsCheckBoxLC[i].IsChecked.Value)
                    lsIdx.Add(i);
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "unknown.csv";
            //saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                m_user.exportLinguisticCategoryAnalysis_Csv(lsIdx, saveFileDialog.FileName);
            }  
        }

        //----------------------------------------------------------------------------------------------------
        // CheckBox_Checked
        //----------------------------------------------------------------------------------------------------
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Handle(sender as CheckBox);
        }

        //----------------------------------------------------------------------------------------------------
        // CheckBox_Unchecked
        //----------------------------------------------------------------------------------------------------
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Handle(sender as CheckBox);
        }

        //----------------------------------------------------------------------------------------------------
        // Handle
        //----------------------------------------------------------------------------------------------------
        void Handle(CheckBox checkBox)
        {
            // Use IsChecked.
            bool flag = checkBox.IsChecked.Value;

            // Assign Window Title.
            //this.Title = "IsChecked = " + flag.ToString();
        }

        //----------------------------------------------------------------------------------------------------
        // btnSummaryTablesExport_Click
        //----------------------------------------------------------------------------------------------------
        private void btnSummaryTablesExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "unknown.csv";            
            if (saveFileDialog.ShowDialog() == true)
            {          
                m_user.exportSummaryTables_Csv(saveFileDialog.FileName);
            }
        }

        //----------------------------------------------------------------------------------------------------
        // btnLexicalItemsExport_Click
        //----------------------------------------------------------------------------------------------------
        private void btnLexicalItemsExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "unknown.csv";
            if (saveFileDialog.ShowDialog() == true)
            {
                m_user.exportLexicalItemsTable_Csv(saveFileDialog.FileName);
            }
        }

        //----------------------------------------------------------------------------------------------------
        // btnChallengeItemsExport_Click
        //----------------------------------------------------------------------------------------------------
        private void btnChallengeItemsExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "unknown.csv";
            if (saveFileDialog.ShowDialog() == true)
            {
                m_user.exportChallengeItemsTable_Csv(saveFileDialog.FileName);
            }
        }

        //----------------------------------------------------------------------------------------------------
        // btnForcedChallengeItemsExport_Click
        //----------------------------------------------------------------------------------------------------
        private void btnForcedChallengeItemsExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "unknown.csv";
            if (saveFileDialog.ShowDialog() == true)
            {
                m_user.exportForcedChallengeItems_Csv(saveFileDialog.FileName);
            }
        }

        //----------------------------------------------------------------------------------------------------
        // btnChallengeItems_ForcedLexicalItems_Click
        //----------------------------------------------------------------------------------------------------
        private void btnChallengeItems_ForcedLexicalItems_Export_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "unknown.csv";
            if (saveFileDialog.ShowDialog() == true)
            {
                m_user.exportChallengeItems_ForcedLexicalItems_Csv(saveFileDialog.FileName);
            }
        }

        //----------------------------------------------------------------------------------------------------
        // btnAllNoiseExport_Click
        //----------------------------------------------------------------------------------------------------
        private void btnAllNoiseExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "unknown.csv";
            if (saveFileDialog.ShowDialog() == true)
            {
                m_user.exportAllNoise_Csv(saveFileDialog.FileName);
            }
        }

        //----------------------------------------------------------------------------------------------------
        // btnNoiseExport_Click
        //----------------------------------------------------------------------------------------------------
        private void btnNoiseExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "unknown.csv";
            if (saveFileDialog.ShowDialog() == true)
            {
                Button btn = (Button)sender;
                if (btn.Name.Equals("btnPhoneVoiceExport"))
                    m_user.exportNoise_Csv(saveFileDialog.FileName, 1);
                else if (btn.Name.Equals("btnNoise1Export"))
                    m_user.exportNoise_Csv(saveFileDialog.FileName, 2);
                else if (btn.Name.Equals("btnNoise2Export"))
                    m_user.exportNoise_Csv(saveFileDialog.FileName, 3);
                else if (btn.Name.Equals("btnNoise3Export"))
                    m_user.exportNoise_Csv(saveFileDialog.FileName, 4);
                else if (btn.Name.Equals("btnNoise4Export"))
                    m_user.exportNoise_Csv(saveFileDialog.FileName, 5);
                else if (btn.Name.Equals("btnNoise5Export"))
                    m_user.exportNoise_Csv(saveFileDialog.FileName, 6);
            }
        }


        private void btnCombineBlockDetail_Click(object sender, RoutedEventArgs e)
        {
            
            List<string> lsXmlFile = new List<string>();

            //string strDirName = "C:\\Users\\Listen-In-user1\\Documents\\my_projects\\listen-in\\data\\2016-08 - rct\\2016-10-24 - pt401\\pt401-4\\ListenIn\\Database\\backup\\";
            string strDirName = "C:\\Users\\Listen-In-user1\\Documents\\my_projects\\listen-in\\data\\2016-08 - rct\\2016-12-14 - pt105\\com.UCL_SoftV.ListenIn_rct_2016_12_13\\files\\ListenIn\\Therapy\\all\\";
            DirectoryInfo dir = new DirectoryInfo(strDirName);
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + strDirName);
            }

            // loop through all the files
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = System.IO.Path.Combine(strDirName, file.Name);
                lsXmlFile.Add(temppath);
            }

            // save all to xml 
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<?xml version='1.0' encoding='utf-8'?>" +
                "<root>" +
                "</root>");

            // Save the document to a file. White space is preserved (no white space).
            string strXmlFileAll = "user_105_therapyblocks_all.xml";

            /*XmlElement xmlChild = doc.CreateElement("userid");
            xmlChild.InnerText = m_strUserId;
            doc.DocumentElement.AppendChild(xmlChild);*/

            List<CBlockDetail> lsBlockDetail = new List<CBlockDetail>();
            //for (int i = 0; i < lsXmlFile.Count; i++)
            for (int i = 0; i < 2269; i++)
            {
                string temppath = System.IO.Path.Combine(strDirName, "user_105_therapyblock_" + i + ".xml");
                if (System.IO.File.Exists(temppath))
                {
                    //XElement root = XElement.Load(lsXmlFile[i]);
                    XElement root = XElement.Load(temppath);
                    lsBlockDetail = (
                        from el in root.Elements("block")
                        select new CBlockDetail
                        {
                            m_intBlockIdx = (int)el.Attribute("idx"),
                            m_lsTrial = (
                                from el2 in el.Elements("trial")
                                select new CTrial
                                {
                                    m_intChallengeItemFeaturesIdx = (int)el2.Attribute("cifIdx"),
                                    m_intChallengeItemIdx = (int)el2.Attribute("ciIdx"),
                                    m_lsStimulus = (
                                        from el3 in el2.Elements("stim")
                                        select new CStimulus
                                        {
                                            intOriginalIdx = (int)el3.Attribute("oriIdx"),
                                            m_strType = (string)el3.Attribute("t"),
                                            m_strPType = (string)el3.Attribute("pt"),
                                        }
                                    ).ToList(),
                                }
                            ).ToList(),
                            m_lsResponse = (
                                from el2 in el.Elements("res")
                                select new CResponse
                                {
                                    m_intScore = (int)el2.Attribute("score"),
                                    m_fRTSec = (float)el2.Attribute("rt"),
                                    m_intReplayBtnCtr = (int)el2.Attribute("replay"),
                                    m_lsSelectedStimulusIdx = (
                                        from el3 in el2.Elements("stim")
                                        select ((int)el3)
                                    ).ToList(),
                                }
                            ).ToList(),
                        }
                    ).ToList();

                    // add to xml all doc
                    for (int m = 0; m < lsBlockDetail.Count; m++)
                    {
                        XmlElement xmlNode = doc.CreateElement("block");
                        XmlAttribute attr = doc.CreateAttribute("idx");
                        attr.Value = i.ToString();
                        xmlNode.SetAttributeNode(attr);

                        for (int k = 0; k < lsBlockDetail[m].m_lsTrial.Count; k++)
                        {
                            CTrial trial = lsBlockDetail[m].m_lsTrial[k];
                            // add trials
                            XmlElement xmlChild2 = doc.CreateElement("trial");
                            XmlAttribute attr2 = doc.CreateAttribute("idx");
                            attr2.Value = k.ToString();
                            xmlChild2.SetAttributeNode(attr2);

                            attr2 = doc.CreateAttribute("ciIdx");
                            attr2.Value = trial.m_intChallengeItemIdx.ToString();
                            xmlChild2.SetAttributeNode(attr2);

                            attr2 = doc.CreateAttribute("cifIdx");
                            attr2.Value = trial.m_intChallengeItemFeaturesIdx.ToString();
                            xmlChild2.SetAttributeNode(attr2);

                            for (var j = 0; j < trial.m_lsStimulus.Count; j++)
                            {
                                XmlElement xmlChild3 = doc.CreateElement("stim");
                                attr2 = doc.CreateAttribute("idx");
                                attr2.Value = j.ToString();
                                xmlChild3.SetAttributeNode(attr2);

                                attr2 = doc.CreateAttribute("oriIdx");
                                attr2.Value = trial.m_lsStimulus[j].intOriginalIdx.ToString();
                                xmlChild3.SetAttributeNode(attr2);

                                attr2 = doc.CreateAttribute("t");
                                attr2.Value = trial.m_lsStimulus[j].m_strType;
                                xmlChild3.SetAttributeNode(attr2);

                                attr2 = doc.CreateAttribute("pt");
                                attr2.Value = trial.m_lsStimulus[j].m_strPType;
                                xmlChild3.SetAttributeNode(attr2);

                                xmlChild2.AppendChild(xmlChild3);
                            }
                            xmlNode.AppendChild(xmlChild2);
                        }

                        for (int k = 0; k < lsBlockDetail[m].m_lsResponse.Count; k++)
                        {
                            CResponse response = lsBlockDetail[m].m_lsResponse[k];

                            // add trials
                            XmlElement xmlChild2 = doc.CreateElement("res");
                            XmlAttribute attr2 = doc.CreateAttribute("idx");
                            attr2.Value = k.ToString();
                            xmlChild2.SetAttributeNode(attr2);

                            attr2 = doc.CreateAttribute("score");
                            attr2.Value = response.m_intScore.ToString();
                            xmlChild2.SetAttributeNode(attr2);

                            attr2 = doc.CreateAttribute("rt");
                            attr2.Value = response.m_fRTSec.ToString();
                            xmlChild2.SetAttributeNode(attr2);

                            attr2 = doc.CreateAttribute("replay");
                            attr2.Value = response.m_intReplayBtnCtr.ToString();
                            xmlChild2.SetAttributeNode(attr2);

                            for (var j = 0; j < response.m_lsSelectedStimulusIdx.Count; j++)
                            {
                                XmlElement xmlChild3 = doc.CreateElement("stim");
                                xmlChild3.InnerText = response.m_lsSelectedStimulusIdx[j].ToString();
                                xmlChild2.AppendChild(xmlChild3);
                            }
                            xmlNode.AppendChild(xmlChild2);
                        }

                        doc.DocumentElement.AppendChild(xmlNode);
                    }

                } // end file exists
                
            } // end for xml files

            // save xml all doc to file
            doc.Save(strXmlFileAll);

        }

    }
}
