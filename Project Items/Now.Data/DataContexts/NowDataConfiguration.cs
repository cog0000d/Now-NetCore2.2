using System;
using System.Collections.Generic;
using Now.Entities.Models.Schedule;
using Now.Entities.Models.Time;
using Type = Now.Entities.Models.Time.Type;

namespace Now.Data.DataContexts
{
    public class NowDataConfiguration
    {
        // scopes define the resources in your system
        public static IEnumerable<Source> GetSources()
        {
            return new List<Source>
            {
                new Source
                {
                    SourceId = new Guid("00000000-0000-0000-0000-000000000001"),
                    TenantId = null,
                    SourceName = "PTFINGER6",
                    SourceDescription = "Injes"
                }
                //new Source{
                //    TenantId = new Guid("00000000-0000-0000-0000-000000000002"),
                //    TenantName = "COGNITIF",
                //    AccountType = AccountType.PrivateGroup,
                //    Description = "Cognitif Group of Companies"
                //},
                //new Source{
                //    TenantId = Guid.NewGuid(),
                //    TenantName = "HERMA",
                //    AccountType = AccountType.PrivateGroup,
                //    Description = "Herma Group of Companies"
                //}
            };
        }

        public static IEnumerable<Type> GetTypes()
        {
            return new List<Type>
            {
                new Type
                {
                    TypeId = new Guid("00000000-0000-0000-0000-000000000001"),
                    TenantId = null,
                    TypeName = "MachineId",
                    TypeDescription = "Injes"
                }
                //new Source{
                //    TenantId = new Guid("00000000-0000-0000-0000-000000000002"),
                //    TenantName = "COGNITIF",
                //    AccountType = AccountType.PrivateGroup,
                //    Description = "Cognitif Group of Companies"
                //},
                //new Source{
                //    TenantId = Guid.NewGuid(),
                //    TenantName = "HERMA",
                //    AccountType = AccountType.PrivateGroup,
                //    Description = "Herma Group of Companies"
                //}
            };
        }

        public static IEnumerable<Shift> GetShifts()
        {
            return new List<Shift>
            {
                new Shift
                {
                    ShiftId = new Guid("00000000-0000-0000-0000-000000000001"),
                    StartTime = DateTimeOffset.Now.DateTime
                }
            };
        }
    }
}