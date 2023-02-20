using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace E_CommerceAPI.API.OwnConfigurations.ColumnWriters
{
    public class UserNameColumnWriter : ColumnWriterBase
    {
        public UserNameColumnWriter() : base(NpgsqlDbType.Varchar)
        {
        }

        public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
        {
           var (userName, value) = logEvent.Properties.FirstOrDefault(p => p.Key == "user_name");

            return value != null ? value.ToString() : null;
        }
    }
}
