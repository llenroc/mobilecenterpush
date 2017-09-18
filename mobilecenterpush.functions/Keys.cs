using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobilecenterpush
{
#error YOU MUST FILL IN YOUR KEYS

	public static class Keys
    {

        public static class MobileCenter
        {
            /// <summary>
            /// The Url of your iOS App in Mobile Center - usually something like:
            /// https://api.mobile.azure.com/v0.1/apps/{username}/{appname}
            /// </summary>
            public static string iOSUrl = "https://api.mobile.azure.com/v0.1/apps/{username}/{appnameios}";
			
            /// <summary>
		    /// The Url of your Android App in Mobile Center - usually something like:
			/// https://api.mobile.azure.com/v0.1/apps/{username}/{appname}
			/// </summary>
            public static string AndroidUrl = "https://api.mobile.azure.com/v0.1/apps/{username}/{appnamedroid}";

            /// <summary>
            /// Mobile Center API Auth Key
            /// </summary>
            public static string AuthKey = "";
        }

        public static class CosmosDB
        {
            /// <summary>
            /// Your CosmosDB Endpoint - fond in the Azure portal
            /// </summary>
            public static string Endpoint = "";

            /// <summary>
            /// Your CosmosDB AuthKey - found in the Azure portal
            /// </summary>
            public static string AuthKey = "";

            /// <summary>
            /// The DatabaseId for the SQL CosmosDB Database
            /// </summary>
            public static string DatabaseId = "";

            /// <summary>
            /// The CollectionID within your Database
            /// </summary>
            public static string TagsCollectionId = "";
        }

    }
}
