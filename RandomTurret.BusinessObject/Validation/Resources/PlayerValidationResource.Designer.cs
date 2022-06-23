﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RandomTurret.BusinessObject.Validation.Resources {
    using System;
    
    
    /// <summary>
    ///   Une classe de ressource fortement typée destinée, entre autres, à la consultation des chaînes localisées.
    /// </summary>
    // Cette classe a été générée automatiquement par la classe StronglyTypedResourceBuilder
    // à l'aide d'un outil, tel que ResGen ou Visual Studio.
    // Pour ajouter ou supprimer un membre, modifiez votre fichier .ResX, puis réexécutez ResGen
    // avec l'option /str ou régénérez votre projet VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class PlayerValidationResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal PlayerValidationResource() {
        }
        
        /// <summary>
        ///   Retourne l'instance ResourceManager mise en cache utilisée par cette classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("RandomTurret.BusinessObject.Validation.Resources.PlayerValidationResource", typeof(PlayerValidationResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Remplace la propriété CurrentUICulture du thread actuel pour toutes
        ///   les recherches de ressources à l'aide de cette classe de ressource fortement typée.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Your e-mail must be valid..
        /// </summary>
        public static string Player_Mail_Format {
            get {
                return ResourceManager.GetString("Player_Mail_Format", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Your e-mail can&apos;t go over 100 characters..
        /// </summary>
        public static string Player_Mail_Max_Length {
            get {
                return ResourceManager.GetString("Player_Mail_Max_Length", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Your e-mail is required..
        /// </summary>
        public static string Player_Mail_Required {
            get {
                return ResourceManager.GetString("Player_Mail_Required", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Your e-mail already exist..
        /// </summary>
        public static string Player_Mail_Unique {
            get {
                return ResourceManager.GetString("Player_Mail_Unique", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Your password must contains at least 1 majuscule, 1 uppercase, 1 lowercase and 1 digit and must be between 8 and 16 characters..
        /// </summary>
        public static string Player_Password_Regex {
            get {
                return ResourceManager.GetString("Player_Password_Regex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Your password is required..
        /// </summary>
        public static string Player_Password_Required {
            get {
                return ResourceManager.GetString("Player_Password_Required", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Your pseudonym must contain a maximum of 20 characters.
        /// </summary>
        public static string Player_Pseudo_Max_Length {
            get {
                return ResourceManager.GetString("Player_Pseudo_Max_Length", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Your pseudonym must be at least 3 characters long..
        /// </summary>
        public static string Player_Pseudo_Min_Length {
            get {
                return ResourceManager.GetString("Player_Pseudo_Min_Length", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Your pseudonym is required..
        /// </summary>
        public static string Player_Pseudo_Required {
            get {
                return ResourceManager.GetString("Player_Pseudo_Required", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à The player object is required during creation..
        /// </summary>
        public static string Player_Required_Create {
            get {
                return ResourceManager.GetString("Player_Required_Create", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à The player you wanted to remove does not exist..
        /// </summary>
        public static string Player_Required_Delete {
            get {
                return ResourceManager.GetString("Player_Required_Delete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Player object is required when modifying..
        /// </summary>
        public static string Player_Required_Update {
            get {
                return ResourceManager.GetString("Player_Required_Update", resourceCulture);
            }
        }
    }
}