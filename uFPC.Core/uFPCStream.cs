using System;
using System.IO;
using System.Text;
using Umbraco.Core.Models;
using uFPC.IO;

namespace uFPC.Stream
{
    public class CustomStream : System.IO.Stream
    {
        private readonly System.IO.Stream filter;
        private ITemplate template;
        private DateTime lastEdited;

        public CustomStream(System.IO.Stream filter, ITemplate template, DateTime lastEdited)
        {
            this.filter = filter;
            this.template = template;
            this.lastEdited = lastEdited;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            string wholeHtmlDocument = uFPCio.GetFromFromCache(template, lastEdited);
            buffer = Encoding.UTF8.GetBytes(wholeHtmlDocument);
            this.filter.Write(buffer, 0, buffer.Length);
        }

        public override void Flush()
        {
            this.filter.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.filter.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.filter.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.filter.Read(buffer, offset, count);
        }

        public override bool CanRead
        {
            get { return this.filter.CanRead; }
        }

        public override bool CanSeek
        {
            get { return this.filter.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return this.filter.CanWrite; }
        }

        public override long Length
        {
            get { return this.filter.Length; }
        }

        public override long Position
        {
            get { return this.filter.Position; }
            set { this.filter.Position = value; }
        }
    }
}