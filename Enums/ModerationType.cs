using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlogProject.Enums
{
    public enum ModerationType
    {
        [Description("Political Propaganda")]
        Political,
        [Description("Inappropriate Language")]
        Language,
        [Description("Drug References")]
        Drugs,
        [Description("Threartening Speech")]
        Threatening,
        [Description("Sexual Explicit Content")]
        Sexual,
        [Description("Hate Speech")]
        HateSpeach,
        [Description("Targeted Shaming")]
        Shaming
    }
}
