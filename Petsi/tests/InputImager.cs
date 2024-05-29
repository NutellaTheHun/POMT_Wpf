using Newtonsoft.Json;
using Petsi.Units;
using Square.Models;

namespace Petsi.tests
{
    /// <summary>
    /// Holds methods for serializing input from sources such as SquareCatalogInput and SquareOrderInput for testing purposes.
    /// </summary>
    public class InputImager
    {
        private const string brorIndexFilePath = "D:/Git-Repos/Petsi/Petsi/tests/SquareOrderInput/BatchOrderResponseImages/fileIndex.txt";
        private const string brorFolderPath = "D:/Git-Repos/Petsi/Petsi/tests/SquareOrderInput/BatchOrderResponseImages/";
        private const string SquareOrderInputFilePath = "D:/Git-Repos//Petsi/Petsi/tests/SquareOrderInput/BaseSquareOrderInput.txt";

        private const string SquareCatalogInputFilePath = "";
        private const string SquareListCatalogFolderPath = "";
        private const string lcrIndexFilePath = "";

        #region BatchRetrieveOrderResponse(bror)

        
        /// <summary>
        /// Serializes the string version of the bror to the given path. Only give path to folder where file resides,
        /// filename is dynamically formatted. 
        /// <para>
        /// WARNING: If bror files exist and you wish to update test files, 
        /// you must delete the existing test files and change the fileIndex.txt value to 0. Otherwise future runs of this function will
        /// add more files from the current index. Because of pagination GetBrorFileIndex() cant differentiate a fresh run for new test files
        /// and a function call between pages.
        /// </para>
        /// </summary>
        /// <param name="bror"> Resulting BatchRetrieveOrderResponse from Square Orders API</param>
        /// <param name="folderPath">path to folder where file exists, !do not include full filepath!</param>
        public static void CollectBatchOrderResponse(BatchRetrieveOrdersResponse bror)
        {
            string fileIndex = GetBrorFileIndex();
            string serializedObj = JsonConvert.SerializeObject(bror);

            File.WriteAllText(BrorFile(fileIndex), serializedObj);
            int index = -1;
            int.TryParse(fileIndex, out index);
            UpdateBrorFileIndex(index);
        }

        /// <summary>
        /// Returns a list of the saved BatchOrderResponse objects. Used for testing.
        /// </summary>
        /// <param name="folderPath">only path to folder of file location, do not include file due to dynamic filenames.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<BatchRetrieveOrdersResponse> GetImageBatchOrderResponse()
        {
            List<BatchRetrieveOrdersResponse> result = new List<BatchRetrieveOrdersResponse>();
            int fileIndex = -1;
            Int32.TryParse(GetBrorFileIndex(), out fileIndex);
            if (fileIndex == -1)
            {
                throw new Exception("GetImageBatchOrderResponse file index parser failed, returned -1");
            }
            string input;
            for (int i = 0; i < fileIndex; i++)
            {
                input = File.ReadAllText(BrorFile(i.ToString()));
                result.Add(JsonConvert.DeserializeObject<BatchRetrieveOrdersResponse>(input));
            }
            return result;
        }

        /// <summary>
        /// Method for uniform use of bror indexed files. 
        /// <para>BatchOrderResponse is called multiple times and must be stored in multiple files to 
        /// follow same structure of original input.</para>
        /// </summary>
        /// <param name="folderPath">specifically the path only to the folder, the name requires formatting and is done in function</param>
        /// <param name="fileIndex"></param>
        /// <returns></returns>
        private static string BrorFile(string fileIndex)
        {
            return brorFolderPath + "bror" + fileIndex + ".txt";
        }
        /// <summary>
        /// Given the current file index, increments by one and writes over fileIndex with new value.
        /// </summary>
        /// <param name="index">current index from Bror file operation</param>
        /// <exception cref="Exception"></exception>
        private static void UpdateBrorFileIndex(int index)
        {
            if (index == -1)
            {
                throw new Exception("InputImager CollectBatchOrderResponse parsed index -1 in UpdateFileIndex");
            }

            int num = index + 1;
            try
            {
                File.WriteAllText(brorIndexFilePath, num.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing file: " + ex.Message);
            }
        }

        /// <summary>
        /// Returns the saved file index of the BatchRetrieveOrderResponse(bror), effectively the count of bror files.
        /// The index is incremented when bror objects are serialized. The "count" is required when deserializing to
        /// control the number of files to be processed.
        /// </summary>
        /// <returns></returns>
        private static string GetBrorFileIndex()
        {
            string index = "unInit";
            try
            {
                if (File.Exists(brorIndexFilePath))
                {
                    index = File.ReadAllText(brorIndexFilePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
            return index;
        }
        #endregion

        /// <summary>
        /// Collects the result of processing input from BatchRetrieveOrderResponse's, used as output to compare in testing.
        /// </summary>
        /// <param name="Orders"></param>
        /// <param name="filepath"></param>
        public static void CollectSquareOrderInput(List<SquareOrderItem> Orders)
        {
            /*
            using (StreamWriter sr = new StreamWriter(filepath))
            {
                foreach (var item in Orders)
                {
                    sr.WriteLine(item.ToString());
                    
                }
            }
            */
            File.WriteAllText(SquareOrderInputFilePath, JsonConvert.SerializeObject(Orders));
        }

       
        //remove filepath arg
        public static void CollectListCatalogResponse(ListCatalogResponse lcr, string filepath)
        {
            throw new NotImplementedException();
        }
        //remove filepath arg
        public static void CollectSquareCatalogInput(List<CatalogItemPetsi> catalogItems, string filepath)
        {
            using (StreamWriter sr = new StreamWriter(filepath))
            {
                foreach (var item in catalogItems)
                {
                    //sr.WriteLine(item.ToString());
                }
            }
        }
    }


}
