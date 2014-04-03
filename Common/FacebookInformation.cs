using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    enum HttpVerb
    {
        GET,
        POST,
        DELETE
    }

    public enum FacebookNotificationPreferences
    {
        POST_TO_WALL_VOTE = 1,
        POST_TO_WALL_NEW_STATION = 2,
        POST_TO_WALL_BM_ARTIST = 3,
        POST_TO_WALL_BM_SONG = 4
    }

    public class FacebookInformation
    {

        public string Email { set; get; }
        public Int32 Id { set; get; }

        public void GetByTokenAuth(string accesstoken)
        {
            if (string.IsNullOrWhiteSpace(accesstoken))
                throw new FacebookException("Debe enviarse un token valido.");

            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("access_token", accesstoken);
            try
            {
                SerializeJSON js = Get("https://graph.facebook.com/me", args);
                Id = Convert.ToInt32(js.Dictionary["id"].String);
                if (!js.Dictionary.ContainsKey("email")) throw new Exception();
                string email = js.Dictionary["email"].String;
                Email = HttpContext.Current.Server.HtmlDecode(email);
                string userName = js.Dictionary["username"].String;
                //if (userName == "") userName = Batanga.Utilities.Utility.buildUserNameFromEmail(email);
                string displayName = string.Empty;
                if (!js.Dictionary.ContainsKey("name")) displayName = email.Substring(0, email.LastIndexOf("@"));
                else displayName = js.Dictionary["name"].String;

                string firstName = string.Empty;
                if (!js.Dictionary.ContainsKey("first_name")) firstName = email.Substring(0, email.LastIndexOf("@"));
                else firstName = js.Dictionary["first_name"].String;
                string lastName = string.Empty;
                if (!js.Dictionary.ContainsKey("last_name")) lastName = "";
                else lastName = js.Dictionary["last_name"].String;
                string gender = js.Dictionary["gender"].String;
                DateTime birthday = Convert.ToDateTime(js.Dictionary["birthday"].String);
                string avatarUrl = "https://graph.facebook.com/" + js.Dictionary["id"].String + "/picture?type=large";
            }
            catch (Exception e)
            {
                throw new FacebookException(e.Message, e);
            }
        }














        /// <summary>
        /// The facebookID used to authenticate API calls.
        /// </summary>
        private string facebookID = string.Empty;
        /// <summary>
        /// The access token used to authenticate API calls.
        /// </summary>
        //public string AccessToken { get; set; }
        private string accessToken = string.Empty;
        public string AccessToken
        {
            get { return accessToken; }
            private set { accessToken = value; }
        }

        /// <summary>
        /// Create a new instance of the API, with public access only.
        /// </summary>
        public FacebookInformation()
            : this(null) { }

        /// <summary>
        /// Create a new instance of the API, using the given token to
        /// authenticate.
        /// </summary>
        /// <param name="token">The access token used for
        /// authentication</param>
        public FacebookInformation(string token)
        {
            AccessToken = token;
        }

        /// <summary>
        /// Makes a Facebook Graph API GET request.
        /// </summary>
        /// <param name="relativePath">The path for the call,
        /// e.g. /username</param>
        public SerializeJSON Get(string relativePath)
        {
            return Call(relativePath, HttpVerb.GET, null);
        }

        /// <summary>
        /// Makes a Facebook Graph API GET request.
        /// </summary>
        /// <param name="relativePath">The path for the call,
        /// e.g. /username</param>
        /// <param name="args">A dictionary of key/value pairs that
        /// will get passed as query arguments.</param>
        public SerializeJSON Get(string relativePath, Dictionary<string, string> args)
        {
            return Call(relativePath, HttpVerb.GET, args);
        }

        /// <summary>
        /// Makes a Facebook Graph API DELETE request.
        /// </summary>
        /// <param name="relativePath">The path for the call,
        /// e.g. /username</param>
        public SerializeJSON Delete(string relativePath)
        {
            return Call(relativePath, HttpVerb.DELETE, null);
        }

        /// <summary>
        /// Makes a Facebook Graph API POST request.
        /// </summary>
        /// <param name="relativePath">The path for the call,
        /// e.g. /username</param>
        /// <param name="args">A dictionary of key/value pairs that
        /// will get passed as query arguments. These determine
        /// what will get set in the graph API.</param>
        public SerializeJSON Post(string relativePath, Dictionary<string, string> args)
        {
            return Call(relativePath, HttpVerb.POST, args);
        }

        /// <summary>
        /// Makes a Facebook Graph API Call.
        /// </summary>
        /// <param name="relativePath">The path for the call, 
        /// e.g. /username</param>
        /// <param name="httpVerb">The HTTP verb to use, e.g.
        /// GET, POST, DELETE</param>
        /// <param name="args">A dictionary of key/value pairs that
        /// will get passed as query arguments.</param>
        private SerializeJSON Call(string relativePath, HttpVerb httpVerb, Dictionary<string, string> args)
        {
            Uri baseURL = new Uri("https://graph.facebook.com");
            Uri url = new Uri(baseURL, relativePath);
            if (args == null)
            {
                args = new Dictionary<string, string>();
            }
            if (!string.IsNullOrEmpty(AccessToken))
            {
                args["access_token"] = AccessToken;
            }
            SerializeJSON obj = SerializeJSON.CreateFromString(MakeRequest(url, httpVerb, args));
            if (obj.IsDictionary && obj.Dictionary.ContainsKey("error"))
            {
                throw new FacebookException(obj.Dictionary["error"]
                                                  .Dictionary["type"]
                                                  .String + " Error: " + obj.Dictionary["error"]
                                                  .Dictionary["message"]
                                                  .String
                                               );
            }
            return obj;
        }

        /// <summary>
        /// Make an HTTP request, with the given query args
        /// </summary>
        /// <param name="url">The URL of the request</param>
        /// <param name="verb">The HTTP verb to use</param>
        /// <param name="args">Dictionary of key/value pairs that represents
        /// the key/value pairs for the request</param>
        private string MakeRequest(Uri url, HttpVerb httpVerb, Dictionary<string, string> args)
        {
            if (args != null && args.Keys.Count > 0 && httpVerb == HttpVerb.GET)
            {
                url = new Uri(url.ToString() + EncodeDictionary(args, true));
            }

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            request.Method = httpVerb.ToString();

            if (httpVerb == HttpVerb.POST)
            {
                string postData = EncodeDictionary(args, false);

                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] postDataBytes = encoding.GetBytes(postData);

                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postDataBytes.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(postDataBytes, 0, postDataBytes.Length);
                requestStream.Close();
            }

            try
            {
                using (HttpWebResponse response
                        = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    return reader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                throw new FacebookException("Server Error", e);
            }
        }

        /// <summary>
        /// Encode a dictionary of key/value pairs as an HTTP query string.
        /// </summary>
        /// <param name="dict">The dictionary to encode</param>
        /// <param name="questionMark">Whether or not to start it
        /// with a question mark (for GET requests)</param>
        private string EncodeDictionary(Dictionary<string, string> dict, bool questionMark)
        {
            StringBuilder sb = new StringBuilder();
            if (questionMark)
            {
                sb.Append("?");
            }
            foreach (KeyValuePair<string, string> kvp in dict)
            {
                sb.Append(HttpUtility.UrlEncode(kvp.Key));
                sb.Append("=");
                sb.Append(HttpUtility.UrlEncode(kvp.Value));
                sb.Append("&");
            }
            sb.Remove(sb.Length - 1, 1); // Remove trailing &
            return sb.ToString();
        }

        public bool CheckUserHavePermissionToPostInWall(int listenerdjid, FacebookNotificationPreferences postType)
        {
            bool havePermission = false;

            return havePermission;
        }

        private string GetPostFeedString(FacebookNotificationPreferences postType)
        {
            string PostFeedText = string.Empty;

            switch (postType)
            {
                case FacebookNotificationPreferences.POST_TO_WALL_VOTE:
                    PostFeedText = "btg_vote";
                    break;
                case FacebookNotificationPreferences.POST_TO_WALL_NEW_STATION:
                    PostFeedText = "btg_create_st";
                    break;
                case FacebookNotificationPreferences.POST_TO_WALL_BM_ARTIST:
                    PostFeedText = "btg_bm_artist";
                    break;
                case FacebookNotificationPreferences.POST_TO_WALL_BM_SONG:
                    PostFeedText = "btg_bm_song";
                    break;

            }

            return PostFeedText;
        }

        public bool IsLinkedToFacebook(int listenerdjid)
        {
            try
            {
                return true;

            }
            catch (Exception ex)
            { return false; }
        }

        private Dictionary<string, string> GetTextForPost(FacebookNotificationPreferences postType)
        {
            Dictionary<string, string> postArgs = new Dictionary<string, string>();

            switch (postType)
            {
                case FacebookNotificationPreferences.POST_TO_WALL_VOTE:
                    postArgs["name"] = HttpContext.GetGlobalResourceObject("social", "FBPostTitleVote").ToString();
                    postArgs["message"] = HttpContext.GetGlobalResourceObject("social", "FBPostMessageVote").ToString();
                    postArgs["picture"] = HttpContext.GetGlobalResourceObject("social", "FBPostPictureVote").ToString();
                    postArgs["caption"] = HttpContext.GetGlobalResourceObject("social", "FBPostCaptionVote").ToString();
                    postArgs["description"] = HttpContext.GetGlobalResourceObject("social", "FBPostDescriptionVote").ToString();
                    break;
                case FacebookNotificationPreferences.POST_TO_WALL_NEW_STATION:
                    postArgs["name"] = HttpContext.GetGlobalResourceObject("social", "FBPostTitleNewStation").ToString();
                    postArgs["message"] = HttpContext.GetGlobalResourceObject("social", "FBPostMessageNewStation").ToString();
                    postArgs["picture"] = HttpContext.GetGlobalResourceObject("social", "FBPostPictureNewStation").ToString();
                    postArgs["caption"] = HttpContext.GetGlobalResourceObject("social", "FBPostCaptionNewStation").ToString();
                    postArgs["description"] = HttpContext.GetGlobalResourceObject("social", "FBPostDescriptionNewStation").ToString();
                    break;
                case FacebookNotificationPreferences.POST_TO_WALL_BM_ARTIST:
                    postArgs["name"] = HttpContext.GetGlobalResourceObject("social", "FBPostTitleBMArtist").ToString();
                    postArgs["message"] = HttpContext.GetGlobalResourceObject("social", "FBPostMessageBMArtist").ToString();
                    postArgs["picture"] = HttpContext.GetGlobalResourceObject("social", "FBPostPictureBMArtist").ToString();
                    postArgs["caption"] = HttpContext.GetGlobalResourceObject("social", "FBPostCaptionBMArtist").ToString();
                    postArgs["description"] = HttpContext.GetGlobalResourceObject("social", "FBPostDescriptionBMArtist").ToString();
                    break;
                case FacebookNotificationPreferences.POST_TO_WALL_BM_SONG:
                    postArgs["name"] = HttpContext.GetGlobalResourceObject("social", "FBPostTitleBMSong").ToString();
                    postArgs["message"] = HttpContext.GetGlobalResourceObject("social", "FBPostMessageBMSong").ToString();
                    postArgs["picture"] = HttpContext.GetGlobalResourceObject("social", "FBPostPictureBMSong").ToString();
                    postArgs["caption"] = HttpContext.GetGlobalResourceObject("social", "FBPostCaptionBMSong").ToString();
                    postArgs["description"] = HttpContext.GetGlobalResourceObject("social", "FBPostDescriptionBMSong").ToString();
                    break;
                default:
                    break;
            }

            return postArgs;
        }

    }

    public class FacebookException : Exception
    {
        public FacebookException()
        {

        }

        public FacebookException(string message)
            : base(message)
        {

        }

        public FacebookException(string message, Exception ex)
            : base(message, ex)
        {

        }
    }
}
