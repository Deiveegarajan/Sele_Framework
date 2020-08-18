using System.Linq;
using Gecko.NCore.Client;
using Gecko.NCore.Client.ObjectModel.V3.En;
using Gecko.NCore.Client.Querying;
using NCoreClientCore.NetStandard;
using Xunit;

namespace XUnitTests
{
    public class BasicTests
    {
        [Fact]
        public void CanQueryOmsForCases()
        {
            var context = GetContext();
            var query = from c in context.Query<Case>().Include(x => x.OfficerName)
                        where c.Id > 10
                        select c;
            var caseDetail = query.Take(40);

        }

        private static IEphorteContext GetContext()
        {
            var options = new NCoreOptions
            {
                NCoreBaseAddress = "https://apps.elementscloud.no/ncore/",
                Database = "934382404_PROD-934382404",
                Username = "sherland",
                Password = "sherland",
                ExternalSystemName = "INTEGRASJON_TEST",
                Role = ""
            };
            var factory = new NCoreFactory(options);
            return factory.Create();
        }
    }
}
