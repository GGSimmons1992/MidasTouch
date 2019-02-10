using MidasTouch.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidasTouch.Tests.HelperTests.HelperModels
{
    class ShareHelperReplica: ShareHelper
    {
        private MidasTouchDBContext _db { get; set; }
        private PortfolioHelperReplica ph { get; set; }

        public ShareHelper()
        {
            _db = new MidasTouchDBContext();
            ph = new PortfolioHelperReplica();
        }
    }
}
