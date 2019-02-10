using MidasTouch.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidasTouch.Tests.HelperTests.HelperModels
{
    class UserHelperReplica: UserHelper
    {
        private MidasTouchDBContext _db { get; set; }
        private PortfolioHelperReplica ph { get; set; }
        private IdentityHelperReplica ih { get; set; }
    }
}
