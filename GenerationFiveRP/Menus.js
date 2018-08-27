var tabindex = null;

/*
Listing de tous les menu du serveur dans un tableau comme suit :
menu[id du tableau][0..13] :
0 - Identification du menu en texte
1 - Le menu en UIMenu
---API.CreateMenu(string title, string subtitle, double x, double y, int anchor, bool enableBanner = true, bool disableControls = false, bool scaleWithSafezone = false);
2 - title, 3 - subtitle, 4 - x, 5 - y, 6 - anchor
7 - enableBanner (true par defaut), 8 - disableControls (false par defaut), 9 - scaleWithSafezone (false par defaut)
---API.SetMenuBannerRectangle(UIMenu menu, int alpha, int red, int green, int blue);
10 - alpha, 11 - red, 12 - green, 13 - blue

menu[id du tableau][14][id de l'item][0..1] :
---API.CreateMenuItem(string label, string description);
0 - label
1 - description

Les menu sont tous creer quand le joueur se connecte et des qu'il en a besoin on lui montre avec
UIMenu.Visible = true; (false pour le cacher)

getIndexOfMenu(index defini en menu[..][0]); pour trouver l'id d'un menu
exemple :
tabindex = getIndexOfMenu("MenuAutoEcole");
menu[tabindex][1].Visible = true;
menu[tabindex][1].Visible = false;
*/

var menu = [
    ["MenuInteVeh", null, "Véhicule", "Choisissez une opération.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Mettre ou enlever sa ceinture", ""],
        ["Démarrer ou éteindre le moteur", ""],
        ["Verrouiller/Déverrouiller les portes", ""],
        ["Garer le vehicule", ""]]
    ],
    ["MenuAutoEcole", null, "Auto-Ecole", "Choisissez une action :", 0, 0, 6, true, 255, 174, 169, 150,
        [["Passer l'épreuve du Code de la route", ""],
        ["Passer l'épreuve de Conduite", ""]]
    ],
    ["MenuVehEboueur", null, "QG des eboueurs", "Choisissez une action :", 0, 0, 6, true, 255, 174, 169, 150,
        [["Ranger le camion-poubelle", ""],
        ["Sortir le camion-poubelle", ""]]
    ],
    ["MenuArmurerieLSPD", null, "Armurerie", "Choisissez votre équipement :", 0, 0, 6, true, 255, 174, 169, 150,
        [["Gilet pare-balles", ""],
        ["Officier probatoire", ""],
        ["Officier", ""],
        ["Inspecteur", ""],
        ["Chef assistant", ""],
        ["Chef de police", ""]]
    ],
    ["MenuDistrib", null, "Distributeur Automatique", "Faites votre choix :", 0, 0, 6, true, 255, 174, 169, 150,
        [["Donuts", ""],
        ["Boissons", ""],
        ["Paquet de chips", ""],
        ["Biscuits", ""]]
    ],
    ["MenuService", null, "Vestiaire du LSPD", "Equipez-vous.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Prendre ou terminer son service", ""],
        ["Equiper sa tenue", ""]]
    ],
    ["MenuCelluleLSPD", null, "Cellules", "Choisissez la porte.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Première porte", ""],
        ["Première cellule", ""],
        ["Deuxième cellule", ""],
        ["Troisième cellule", ""],
        ["Porte du fond", ""]]
    ],
    ["MenuArmurerieCivil", null, "Armurerie", "Faites votre choix.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Pistolet 9mm", ""],
        ["Mousquet", ""],
        ["Couteau de combat", ""]]
    ],
    ["MenuBanque", null, "Banque de Los Santos", "Choisissez une opération.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Consulter son solde", ""],
        ["Retirer de l'argent", ""],
        ["Deposer de l'argent", ""]]
    ],
    ["MenuBanque", null, "Magasin de Los Santos", "Choisissez une opération.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Paquet de cigarettes", ""],
        ["Briquet", ""],
        ["Bouteille d'alcool", ""],
        ["Téléphone", ""],
        ["Puce prépayée", ""]]
    ],
    ["MenuRevendeur", null, "Revendeur", "Choisissez une opération.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Kit d'outils", ""],
        ["Scie", ""],
        ["Pied de biche", ""],
        ["Batte de baseball", ""],
        ["Pince Monseigneur", ""]]
    ],
    ["MenuMaisonAchetéeProprio", null, "Votre Logement", "Choisissez une action.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Entrer", ""],
        ["Verrouiller/Déverrouiller", ""]]
    ],
    ["MenuSortirLogement", null, "Votre Logement", "Choisissez une action.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Sortir", ""],
        ["Accès garage", ""]]
    ],
    ["MenuMaisonAchetéeNonProprio", null, "Logement", "Choisissez une action.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Entrer", ""],
        ["Sonner", ""],
        ["Boite aux lettres", ""]]
    ],
    ["MenuMaisonAVendre", null, "Logement En Vente", "Choisissez une action.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Visiter", ""],
        ["Prix", ""],
        ["Acheter", ""]]
    ],
    ["MenuHackeur", null, "Ordinateur", "Choisissez une action.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Tracer un numéro de téléphone", "Est utile pour retrouver le propriétaire d'un numéro de téléphone."],
        ["Récupérer des données bancaires", "Doit être utiliser avant d'encoder une carte de crédit."],
        ["Encoder une carte de crédit", "Sert à encoder une carte de crédit."]]
    ],
    ["MenuPNJHackeur", null, "Planque", "Faites votre choix.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Ordinateur", ""],
        ["Routeur modem", "Nécessite une puce pour fonctionner."],
        ["Puce 4G", ""]]
    ],
    ["MenuExteVeh", null, "Véhicule", "Selectionnez une action.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Ouvrir le capot", ""],
        ["Verrouiller/Déverrouiller les portes", ""],
        ["Ouvrir le coffre", ""],
        ["Contenu du coffre", ""]]
    ],
    ["MenuExtToGarage", null, "Menu Garage", "Choisissez une action.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Entrer", ""],
        ["Verrouiller/Déverrouiller Garage", ""]]
    ],
    ["MenuGarageToExt", null, "Menu Garage", "Choisissez une action.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Sortir", ""],
        ["Verrouiller/Déverrouiller Garage", ""]]
    ],
    ["MenuUniteLSPD", null, "Menu Unite LSPD", "Choisissez une action.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Début de patrouille", ""],
        ["Fin de patrouille", ""],
        ["Unite en Stand-By", ""]]
    ],
    ["MenuVehConcess", null, "Concessionnaire", "Faites votre choix.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Informations", ""],
        ["Acheter", ""]]
    ],
    ["MenuFournisseur", null, "Fournisseur", "Choisis quoi importer dans ta planque.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Kit Pistolet", "Assez pour monter 10 armes au coût de 5.000$"],
        ["Kit Pistolet-Mitrailleur", "Assez pour monter 10 armes au coût de 6.000$"],
        ["Kit Fusil à Pompe", "Assez pour monter 10 armes au coût de 7.500$"],
        ["Kit Fusil d'Assaut", "Assez pour monter 10 armes au coût de 10.000$"]]
    ],
    ["KitPistolet", null, "Kit Pistolet", "Choisissez un modèle à assembler.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Berretta 92FS", ""],
        ["Glock 18", ""],
        ["Desert Eagle", ""],
        ["Heckler & Koch P7", ""],
        ["Colt M1911", ""]]
    ],
    ["KitPMitr", null, "Kit Pistolet-Mitrailleur", "Choisissez un modèle à assembler.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Uzi", ""],
        ["TEC-9", ""],
        ["HK MP5", ""],
        ["SIG MPX", ""],
        ["Skorpion", ""]]
    ],
    ["KitPompe", null, "Kit fusil à pompe", "Choisissez un modèle à assembler.", 0, 0, 6, true, 255, 174, 169, 150,
        [["Mossberg 500", ""],
        ["Colt Model 1883", ""],
        ["Mousquet", ""],
        ["Double-barreled shotgun", ""]]
    ],
    ["KitFusil", null, "Kit fusil d'assaut", "Choisissez un modèle à assembler.", 0, 0, 6, true, 255, 174, 169, 150,
        [["AK-47", ""],
        ["AR-15", ""],
        ["Heckler & Koch G36C", ""],
        ["Norinco Type 86S", ""],
        ["AKMSU", ""]]
    ],
    ["MenuMagasin", null, "Magasin de Los Santos", "Choisissez une opération.", 0, 0, 6, true, 255, 174, 169, 150,
    [["Paquet de cigarettes", ""],
    ["Briquet", ""],
    ["Bouteille d'alcool", ""],
    ["Téléphone", ""],
    ["Puce prépayée", ""]]
    ]
];

function getIndexOfMenu(string) {
    for (var i = 0; i < menu.length; i++) {
        var index = menu[i].indexOf(string);
        if (index > -1) {
            return i;
        }
    }
}

function ItemSelected(indexdutableau, index)
{
    var titre = menu[tabindex][0];
	API.triggerServerEvent(titre, index);
}

API.onResourceStart.connect(function () {
    for (i = 0; i < menu.length; i++) {
        menu[i][1] = API.createMenu(menu[i][2], menu[i][3], menu[i][4], menu[i][5], menu[i][6], menu[i][7]);
        API.setMenuBannerRectangle(menu[i][1], menu[i][8], menu[i][9], menu[i][10], menu[i][11]);
        for (u = 0; u < menu[i][12].length; u++) {
            menu[i][1].AddItem(API.createMenuItem(menu[i][12][u][0], menu[i][12][u][1]));
        }
        menu[i][1].Visible = false;
        menu[i][1].OnItemSelect.connect(function (sender, selecteditem, index) {            
            sender.Visible = false;
            ItemSelected(tabindex, index);
            tabindex = null;
        });
    }
});

API.onServerEventTrigger.connect(function (eventName, args) {
    if (tabindex !== null)
    {
        menu[tabindex][1].Visible = false;
        tabindex = null;
    }

    switch (eventName) {
        case "MenuAutoEcole":
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            API.sendChatMessage("~#aea996~", "Auto-Ecole");
            API.sendChatMessage("Bonjour, que puis-je faire pour vous?");
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            break;
        case "MenuArmurerieLSPD":
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            API.sendChatMessage("~#aea996~", "Armurerie du LSPD");
            API.sendChatMessage("Prenez votre équipement de service.");
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            break;
        case "MenuDistrib":
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            API.sendChatMessage("~#aea996~", "Distributeur automatique");
            API.sendChatMessage("Entrez le numéro de votre choix et insérez la somme.");
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            break;
        case "MenuService":
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            API.sendChatMessage("~#aea996~", "Vestiaire du LSPD");
            API.sendChatMessage("Prenez votre service et équipez-vous de votre tenue.");
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            break;
        case "MenuCelluleLSPD":
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            API.sendChatMessage("~#aea996~", "Gestion des cellules");
            API.sendChatMessage("Quelles portes souhaitez-vous ouvrir ?");
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            break;
        case "MenuArmurerieCivil":
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            API.sendChatMessage("~#aea996~", "Armurerie");
            API.sendChatMessage("Bienvenue à l'armurerie de Los Santos, que puis-je faire pour vous ?");
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            break;
        case "MenuBanque":
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            API.sendChatMessage("~#aea996~", "Banque de Los Santos");
            API.sendChatMessage("Effectuez vos opérations bancaires auprès de l'employé au guichet.");
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            break;
        case "MenuMagasin":
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            API.sendChatMessage("~#aea996~", "Magasin de Los Santos");
            API.sendChatMessage("Bienvenue dans notre magasin, faites vos courses à moindre prix.");
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            break;
        case "MenuRevendeur":
            API.sendChatMessage("Revendeur: Si tu cherches quelque chose, je dois sûrement l'avoir ici.");
            break;
        case "MenuHackeur":
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            API.sendChatMessage("~#aea996~", "Ordinateur portable");
            API.sendChatMessage("Bienvenue sur la page d'accueil, que souhaitez-vous faire ?");
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            break;
        case "MenuPNJHackeur":
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            API.sendChatMessage("~#aea996~", "Planque des hackeurs");
            API.sendChatMessage("Bienvenue à la planque, tu dois garder le silence sur notre emplacement.");
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            break;
        case "MenuVehConcess":
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            API.sendChatMessage("~#aea996~", "Concessionnaire");
            API.sendChatMessage("Bienvenue à la concession, ici nos véhicules sont à prix abordable.");
            API.sendChatMessage("~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - -");
            break;
        case "MenuVehEboueur":
        case "MenuMaisonAchetéeProprio":
        case "MenuSortirLogement":
        case "MenuMaisonAchetéeNonProprio":
        case "MenuMaisonAVendre":
        case "MenuExteVeh":
        case "MenuExtToGarage":
        case "MenuGarageToExt":
        case "MenuUniteLSPD":
        case "MenuInteVeh":
        case "MenuFournisseur":
        case "KitPistolet":
        case "KitPMitr":
        case "KitPompe":
        case "KitFusil":
            break;
        default:
            return;
    }
    tabindex = getIndexOfMenu(eventName);
    menu[tabindex][1].Visible = true;

});