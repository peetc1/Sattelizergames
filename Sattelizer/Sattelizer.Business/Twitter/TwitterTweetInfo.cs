using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace Sattelizer.Business.Twitter
{
    public class TwitterTweetInfo
    {
        public IEnumerable<TwitterStatus> Tweets { get; set; }
        public TwitterUser User { get; set; }
    }
}
