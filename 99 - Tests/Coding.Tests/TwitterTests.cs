using System;
using System.Linq;
using LinqToTwitter;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coding.Tests
{
    /// <summary>
    /// Tests to twitter API.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy - Rerum</Author>
    /// <Date>10/1/2016 10:39:57 PM</Date>
    [TestClass]
    public class TwitterTests
    {
        /// <summary>
        /// Consumer Key (API Key).
        /// </summary>
        private const string ConsumerKey = "";

        /// <summary>
        /// Consumer Secret (API Secret).
        /// </summary>
        private const string ConsumerSecret = "";

        /// <summary>
        /// Access Token.
        /// </summary>
        private const string AccessToken = "";

        /// <summary>
        /// Access Token Secret.
        /// </summary>
        private const string AccessTokenSecret = "";

        /// <summary>
        /// Gets the tweets by the received term.
        /// </summary>
        /// <param name="term">term to be searched.</param>
        /// <param name="count">amount of tweets.</param>
        /// <returns>tweets list.</returns>
        private List<string> GetTweets(string term, int count)
        {
            // Tweets list
            var tweets = new List<string>();

            // Authenticating in Twitter account.
            var auth = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = ConsumerKey,
                    ConsumerSecret = ConsumerSecret,
                    AccessToken = AccessToken,
                    AccessTokenSecret = AccessTokenSecret
                }
            };
            var authorizeTask = auth.AuthorizeAsync();
            authorizeTask.Wait();

            // Gets the search term.
            var twitterContext = new TwitterContext(auth);
            Search values = (from search in twitterContext.Search
                             where search.Query == term &&
                                   search.Count == count &&
                                   search.IncludeEntities == true &&
                                   search.Type == SearchType.Search &&
                                   search.ResultType == ResultType.Recent
                             select search).SingleOrDefault();

            // Mounts the tweets list.
            if (values != null && values.Statuses != null)
            {
                values.Statuses.ForEach(tweet =>
                {
                    tweets.Add(tweet.Text);
                });
            }

            return tweets;
        }

        /// <summary>
        /// Twitter API - get tweets.
        /// </summary>
        [TestMethod]
        public void GetTweetsTest()
        {
            foreach (var tweet in GetTweets("Eleição", 10))
            {
                System.Diagnostics.Debug.WriteLine(tweet);
            }
        }
    }
}
