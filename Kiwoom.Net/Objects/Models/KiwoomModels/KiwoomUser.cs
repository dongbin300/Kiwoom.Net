using System.Collections.Generic;

namespace Kiwoom.Net.Objects.Models.KiwoomModels
{
    public class KiwoomUser
    {
        public int AccountCount { get; set; }
        public IEnumerable<string> AccountNumbers { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsKeyboardSecurity { get; set; }
        public bool IsFirewall { get; set; }
    }
}
