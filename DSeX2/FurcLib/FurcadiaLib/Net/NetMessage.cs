/*Log Header
 *Format: (date,Version) AuthorName, Changes.
 * (Oct 27,2009,0.1) Squizzle, Initial Developer.
 * 
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Furcadia.Net
{
    /// <summary>
    /// Default.
    /// </summary>
    public class NetMessage : INetMessage
    {
        private StringBuilder _data;
        public NetMessage()
        {
            _data = new StringBuilder();
        }

        public string GetString()
        {
            return _data.ToString();
        }

        public void Write(string data)
        {
            _data.Append(data);
        }

        public void Write(byte[] data)
        {
            _data.Append(Encoding.GetEncoding(NetProxy.EncoderPage).GetString(data));
        }
    }
}