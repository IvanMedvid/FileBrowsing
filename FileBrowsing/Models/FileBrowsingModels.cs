using System.Collections.Generic;
using Newtonsoft.Json;

namespace FileBrowsing.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FileBrowsingModels
    {
        [JsonProperty(PropertyName = "directories")]
        public List<string> Directories { get; set; }
        [JsonProperty(PropertyName = "files")]
        public List<string> Files { get; set; }
        [JsonProperty(PropertyName = "countFilesWithSizeLessTenMb")]
        public int CoutnFilesWithSizeLessTenMb { get; set; }
        [JsonProperty(PropertyName = "countFilesWithSizeMoreTenMb")]
        public int CoutnFilesWithSizeMoreTenMb { get; set; }
        [JsonProperty(PropertyName = "countFilesWithSizeMoreHundredMb")]
        public int CoutnFilesWithSizeMoreHundredMb { get; set; }
        [JsonProperty(PropertyName = "errorMessage")]
        public string ErrorMessage { get; set; }
        [JsonProperty(PropertyName = "currentDirectory")]
        public string CurrentDirectory { get; set; }
        [JsonProperty(PropertyName = "parentDirectory")]
        public string ParentDirectory { get; set; }
    }
}
