using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Sattelizer.Business.Twitter;
using TweetSharp;

namespace Sattelizer.Business
{
    public class TwitterUtility
    {

        #region Constants
        private const string Satt = "@SattelizerGames";
        private const string BearerToken = "oauth2/token";
        #endregion

        private readonly string _consumerSecret;
        private readonly string _consumerKey;
        private readonly string _accessSecret;
        private readonly string _accessToken;

        private TwitterService _service;
        private TwitterUser _user;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterUtility"/> class. And authenticates to Twitter.
        /// </summary>
        public TwitterUtility()
        {
            // setup all tokens and secrets
            _consumerKey = ConfigurationManager.AppSettings["twitterKey"];
            _consumerSecret = ConfigurationManager.AppSettings["twitterSecret"];
            _accessToken = ConfigurationManager.AppSettings["twitterAccessToken"];
            _accessSecret = ConfigurationManager.AppSettings["twitterAccessSecret"];


            // instantiate and authenticate
            _service = new TwitterService(_consumerKey, _consumerSecret);
            _service.AuthenticateWith(_accessToken, _accessSecret);

            // get user profile
            _user = _service.GetUserProfileFor(new GetUserProfileForOptions() {ScreenName = Satt});
        }


        /// <summary>
        /// Gets the tweets.
        /// </summary>
        /// <returns>IEnumerable&lt;TwitterStatus&gt;.</returns>
        public TwitterTweetInfo GetTweets()
        {
            // setup tweet options
            var options = new ListTweetsOnUserTimelineOptions
            {
                ScreenName = Satt, 
                Count = 20
            };

            // make call
            var tweets = _service.ListTweetsOnUserTimeline(options);

            var info = new TwitterTweetInfo();

            info.Tweets = tweets;
            info.User = _service.GetUserProfileFor(new GetUserProfileForOptions { ScreenName = Satt });

            //info.User.ProfileImageUrl

            return info;
        }

        /*

        
        private string _bearer

        #region Constants
        private const string BaseAddress = "https://api.twitter.com/";
        private const string BearerToken = "oauth2/token";
        #endregion

        

        public async void Authenticate()
        {
            using (var client = new HttpClient())
            {
                // set base info
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type",
                    "application/x-www-form-urlencoded;charset=UTF-8");
                
                // generate base key string and encode
                var key = String.Format("{0}:{1}", _consumerKey, _consumerSecret);
                var byteArray = Encoding.ASCII.GetBytes(key);
                var encoded = Convert.ToBase64String(byteArray);
            
                // add auth header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);

                // setup call
                var content = new StringContent("grant_type=client_credentials");

                // make call
                var response = await client.PostAsync(BearerToken, content);

                // parse response
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsAsync<TokenResponse>();

            }
        }

        
        internal class TokenResponse
        {
            public string token_type { get; set; }
            public string access_token { get; set; }
        }
        */
    }
}
