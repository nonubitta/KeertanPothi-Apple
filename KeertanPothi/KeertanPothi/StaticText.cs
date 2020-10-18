using System;
using System.Collections.Generic;
using System.Text;

namespace KeertanPothi
{
    internal class StaticText
    {
        internal class MenuText : StaticTextBase
        {
            public string MenuSearch { get; }
            public string MenuRandom { get; }
            public string MenuKeertanPothi { get; }
            public string MenuSundarGutka { get; }
            public string MenuWriterList { get; }
            public string MenuRaagList { get; }
            public string MenuHistory { get; }
            public string MenuHelpContact { get; }
            public int FontSize { get; }

            public MenuText()
            {
                if (IsPunjabi)
                {
                    MenuSearch = "ਸ਼ਬਦ ਖੋਜ";
                    MenuRandom = "ਬੇਤਰਤੀਬ ਸ਼ਬਦ";
                    MenuKeertanPothi = "ਕੀਰਤਨ ਪੋਥੀ";
                    MenuSundarGutka = "ਸੁੰਦਰ ਗੁਟਕਾ";
                    MenuWriterList = "ਲੇਖਕ ਸੂਚੀ";
                    MenuRaagList = "ਰਾਗ  ਸੂਚੀ";
                    MenuHistory = "ਖੋਜੇ ਸ਼ਬਦ";
                    MenuHelpContact = "ਮਦਦ/ਸੰਪਰਕ";
                    FontSize = 24;
                }
                else
                {
                    MenuSearch = "Search";
                    MenuRandom = "Random Shabad";
                    MenuKeertanPothi = "Keertan Pothi";
                    MenuSundarGutka = "Sundar Gutka";
                    MenuWriterList = "Writer List";
                    MenuRaagList = "Raag List";
                    MenuHistory = "History";
                    MenuHelpContact = "Help/Contact";
                    FontSize = 21;
                }
            }
        }
  
        internal class SearchText: StaticTextBase
        {
            public string ThreeLetterInfo { get; }
            public string HowToHelpInfo { get; }
            public string DDLFirstLetterSearch { get; }
            public string DDLMainLetterSearch { get; }
            public string DDLExactSearch { get; }
            public string DDLAngSearch { get; }
            
            public SearchText()
            {
                if (IsPunjabi)
                {
                    ThreeLetterInfo = "* ਨਤੀਜੇ ਵੇਖਣੇ ਸ਼ੁਰੂ ਕਰਨ ਲਈ 3 ਅੱਖਰ ਟਾਈਪ ਕਰੋ";
                    HowToHelpInfo = "ਕਿਸ ਤਰ੍ਹਾਂ ਖੋਜ ਕੀਤੀ ਜਾ ਸਕਦੀ ਹੈ ਦੀ ਸਹਾਇਤਾ ਲਈ ਹੇਠਾਂ ਦਿੱਤੇ 'Help' ਬਟਨ 'ਤੇ ਕਲਿਕ ਕਰੋ";
                    DDLFirstLetterSearch = "ਪਹਿਲੇ ਅਖਰ ਤੋਂ ਖੋਜ";
                    DDLMainLetterSearch = "ਬਿਨਾ ਲਗਾ ਮਾਤਰਾ ਤੋਂ ਖੋਜ";
                    DDLExactSearch = "Exact Search";
                    DDLAngSearch = "ਅੰਗ ਖੋਜ";
                }
                else
                {
                    ThreeLetterInfo = "* Type 3 letters to start seeing results";
                    HowToHelpInfo = "Click on 'Help' button below to access help on how to search";
                    DDLFirstLetterSearch = "First Letter Search";
                    DDLMainLetterSearch = "Main Letter Search";
                    DDLExactSearch = "Exact Search";
                    DDLAngSearch = "Ang Search";
                }
            }
        }

        internal class PothiText : StaticTextBase
        {
            public string RenameDelete { get; }
            public string NoPothisFound { get; }
            public string NewPothiNameSubject { get; }
            public string NewPothiNameMsg { get; }
            public PothiText()
            {
                if (IsPunjabi)
                {
                    RenameDelete = "* ਨਾਮ ਬਦਲਣ ਜਾਂ ਮਿਟਾਉਣ ਲਈ ਪੋਥੀ ਨੂੰ ਲੰਮੇ ਸਮੇਂ ਲਈ ਦਬਾਓ";
                    NoPothisFound = "ਤੁਸੀਂ ਕੋਈ ਪੋਥੀ ਨਹੀਂ ਬਣਾਈ ਹੈ|" + Environment.NewLine + "ਨਵੀਂ ਪੋਥੀ ਬਣਾਉਣ ਲਈ," + Environment.NewLine + "ਸੱਜੇ ਤੋਂ ਉੱਪਰ ਦੇ ਪਲੱਸ (+) ਬਟਨ ਤੇ ਕਲਿਕ ਕਰੋ";
                    NewPothiNameSubject = "ਨਵੀਂ ਪੋਥੀ ਬਣਾਓ";
                    NewPothiNameMsg = "ਨਵੀਂ ਪੋਥੀ ਦਾ ਨਾਮ";
                }
                else
                {
                    RenameDelete = "* Long press a pothi to rename or delete";
                    NoPothisFound = "No pothis found." + Environment.NewLine + "Click on plus button in top right" + Environment.NewLine + "to create your first pothi.";
                    NewPothiNameSubject = "Add new Pothi";
                    NewPothiNameMsg = "Enter pothi name";
                }

            }
        }

        internal class Settings : StaticTextBase
        {
            public string ThemeBlue { get; set; }
            public string ThemeGray { get; set; }
            public string ThemeBlack { get; set; }
            public string ThemeFblack { get; set; }
            public Settings()
            {
                if (IsPunjabi)
                {
                   
                }
                else
                {
                    
                }

            }
        }

        internal class PothiShabadsText : StaticTextBase
        {
            public string NoShabadsAdded1 { get; }
            public string NoShabadsAdded2 { get; }
            public string NoShabadsAdded3 { get; }
            public string ButtonText { get; set; }

            public PothiShabadsText()
            {
                if (IsPunjabi)
                {
                    NoShabadsAdded1 = "ਤੁਸੀਂ ਇਸ ਪੋਥੀ ਵਿਚ ਕੋਈ ਸ਼ਬਦ ਸ਼ਾਮਲ ਨਹੀਂ ਕੀਤਾ ਹੈ";
                    NoShabadsAdded2 = "ਸ਼ਬਦ ਸਕ੍ਰੀਨ ਤੋਂ, ਕਲਿੱਕ ਕਰੋ";
                    NoShabadsAdded3 = "ਸ਼ਬਦ ਨੂੰ ਪੋਥੀ ਵਿਚ ਸ਼ਾਮਲ ਕਰਨ ਲਈ ";
                    ButtonText = "ਪੋਥੀ ਵਿਚ ਜੋੜਨ ਲਈ ਸ਼ਬਦ ਖੋਜ ਕਰੋ";
                }
                else
                {
                    NoShabadsAdded1 = "You haven't added any shabad to this pothi.";
                    NoShabadsAdded2 = "From shabad screen, click ";
                    NoShabadsAdded3 = "icon, to add shabad to pothi";
                    ButtonText = "Search Shabad to add to this Pothi";
                }

            }
        }

        internal abstract class StaticTextBase
        {
            public bool IsEnglish { get; set; }
            public bool IsPunjabi { get; set; }
            public StaticTextBase()
            {
                string language = Util.PrefSelectedLanguage;
                switch (language)
                {
                    case "E":
                        IsEnglish = true;
                        IsPunjabi = false;
                        break;
                    case "P":
                        IsEnglish = false;
                        IsPunjabi = true;
                        break;
                    default:
                        IsEnglish = true;
                        IsPunjabi = false;
                        break;
                }
            }
        }
    }
}
