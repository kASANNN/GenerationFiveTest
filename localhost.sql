-- phpMyAdmin SQL Dump
-- version 4.2.12deb2+deb8u2
-- http://www.phpmyadmin.net
--
-- Client :  localhost
-- Généré le :  Jeu 31 Août 2017 à 20:32
-- Version du serveur :  5.5.57-0+deb8u1
-- Version de PHP :  5.6.30-0+deb8u1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de données :  `generationfive`
--

-- --------------------------------------------------------

--
-- Structure de la table `ATM`
--

CREATE TABLE IF NOT EXISTS `ATM` (
`idATM` int(11) NOT NULL,
  `PosX` float(8,4) DEFAULT NULL,
  `PosY` float(8,4) DEFAULT NULL,
  `PosZ` float(8,4) DEFAULT NULL,
  `Argent` int(10) DEFAULT NULL
) ENGINE=MyISAM AUTO_INCREMENT=16 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `ATM`
--

INSERT INTO `ATM` (`idATM`, `PosX`, `PosY`, `PosZ`, `Argent`) VALUES
(1, 288.0000, -1256.0000, 29.0000, 50000),
(2, 288.0000, -1282.0000, 29.0000, 50000),
(3, 33.0000, -1348.0000, 29.0000, 50000),
(4, -56.0000, -1752.0000, 29.0000, 50000),
(5, 129.0000, -1291.0000, 29.0000, 50000),
(6, 296.0000, -894.0000, 29.0000, 50000),
(7, 295.0000, -896.0000, 29.0000, 50000),
(8, 111.0000, -775.0000, 31.0000, 50000),
(9, 114.0000, -776.0000, 31.0000, 50000),
(10, 112.0000, -819.0000, 31.0000, 50000),
(11, 5.0000, -919.0000, 29.0000, 50000),
(12, 24.0000, -946.0000, 29.0000, 50000),
(13, -254.0000, -692.0000, 33.0000, 50000),
(14, -256.0000, -715.0000, 33.0000, 50000),
(15, -258.0000, -723.0000, 33.0000, 50000);

-- --------------------------------------------------------

--
-- Structure de la table `CheckpointAutoEcole`
--

CREATE TABLE IF NOT EXISTS `CheckpointAutoEcole` (
`ID` int(11) NOT NULL,
  `PosX` float(8,4) NOT NULL,
  `PosY` float(8,4) NOT NULL,
  `PosZ` float(8,4) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `CheckpointAutoEcole`
--

INSERT INTO `CheckpointAutoEcole` (`ID`, `PosX`, `PosY`, `PosZ`) VALUES
(1, 209.0614, -1601.6281, 28.6276),
(2, 304.7900, -1532.5229, 28.4979),
(3, 430.7139, -1625.8030, 28.5172),
(4, 370.4160, -1723.7920, 28.5900),
(5, 290.4863, -1677.5240, 28.6005),
(6, 256.8976, -1643.2870, 28.4513);

-- --------------------------------------------------------

--
-- Structure de la table `ClefsLogements`
--

CREATE TABLE IF NOT EXISTS `ClefsLogements` (
`ID` int(11) NOT NULL,
  `IDLogement` int(11) DEFAULT NULL,
  `IDJoueur` int(11) DEFAULT NULL
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `ClefsLogements`
--

INSERT INTO `ClefsLogements` (`ID`, `IDLogement`, `IDJoueur`) VALUES
(1, 1, 19),
(2, 4, 19),
(3, 1, 2),
(4, 2, 21);

-- --------------------------------------------------------

--
-- Structure de la table `ClefsVehicules`
--

CREATE TABLE IF NOT EXISTS `ClefsVehicules` (
`ID` int(11) NOT NULL,
  `IDVehicule` int(11) DEFAULT NULL,
  `IDJoueur` int(11) DEFAULT NULL,
  `Nombre` int(11) NOT NULL
) ENGINE=MyISAM AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `ClefsVehicules`
--

INSERT INTO `ClefsVehicules` (`ID`, `IDVehicule`, `IDJoueur`, `Nombre`) VALUES
(1, 92, 19, 1),
(2, 98, 19, 1),
(3, 0, 19, 1),
(4, 98, 18, 1),
(5, 92, 18, 1),
(6, 0, 19, 1),
(7, 103, 21, 1);

-- --------------------------------------------------------

--
-- Structure de la table `Coffre`
--

CREATE TABLE IF NOT EXISTS `Coffre` (
  `id` int(11) NOT NULL,
  `type` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `Coffre`
--

INSERT INTO `Coffre` (`id`, `type`) VALUES
(1, 1);

-- --------------------------------------------------------

--
-- Structure de la table `Concess`
--

CREATE TABLE IF NOT EXISTS `Concess` (
`ID` int(11) NOT NULL,
  `IDVeh` int(11) DEFAULT NULL,
  `Vendu` tinyint(4) DEFAULT NULL,
  `Prix` int(11) DEFAULT NULL,
  `PosX` float(60,30) NOT NULL,
  `PosY` float(60,30) NOT NULL,
  `PosZ` float(60,30) NOT NULL,
  `RotX` float(60,30) NOT NULL,
  `RotY` float(60,30) NOT NULL,
  `RotZ` float(60,30) NOT NULL
) ENGINE=MyISAM AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `Concess`
--

INSERT INTO `Concess` (`ID`, `IDVeh`, `Vendu`, `Prix`, `PosX`, `PosY`, `PosZ`, `RotX`, `RotY`, `RotZ`) VALUES
(1, 0, 0, 0, -59.232810974121094000000000000000, -1686.355957031250000000000000000000, 28.817510604858400000000000000000, 0.028176059946417810000000000000, -0.000332496594637632370000000000, -130.025405883789060000000000000000),
(2, 0, 0, 0, -56.694229125976560000000000000000, -1683.593994140625000000000000000000, 28.817419052124023000000000000000, 0.008619846776127815000000000000, 0.000774409680161625100000000000, -130.118606567382800000000000000000),
(3, 0, 0, 0, -54.337310791015625000000000000000, -1680.437011718750000000000000000000, 28.788450241088867000000000000000, 0.295129388570785500000000000000, -1.427521944046020500000000000000, -129.172500610351560000000000000000),
(4, 0, 0, 0, -51.753818511962890000000000000000, -1677.413940429687500000000000000000, 28.684700012207030000000000000000, 0.577387928962707500000000000000, -1.532026052474975600000000000000, -128.787902832031250000000000000000),
(5, 0, 0, 0, -51.076988220214844000000000000000, -1693.630004882812500000000000000000, 28.817710876464844000000000000000, 0.023039270192384720000000000000, -0.024320149794220924000000000000, 48.156620025634766000000000000000),
(6, 0, 0, 0, -47.142429351806640000000000000000, -1691.642944335937500000000000000000, 28.755939483642578000000000000000, 0.167064398527145390000000000000, 1.238178968429565400000000000000, 47.872680664062500000000000000000),
(7, 0, 0, 0, -43.776279449462890000000000000000, -1689.337036132812500000000000000000, 28.698549270629883000000000000000, 0.638385713100433300000000000000, -0.137684300541877750000000000000, 48.818080902099610000000000000000);

-- --------------------------------------------------------

--
-- Structure de la table `Entreprises`
--

CREATE TABLE IF NOT EXISTS `Entreprises` (
`ID` int(11) NOT NULL,
  `Nom` text,
  `IdScript` int(11) DEFAULT NULL,
  `Coffre` int(11) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `Factions`
--

CREATE TABLE IF NOT EXISTS `Factions` (
  `ID` int(11) NOT NULL,
  `Nom` text NOT NULL,
  `IDScript1` int(11) NOT NULL,
  `IDScript2` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `Garages`
--

CREATE TABLE IF NOT EXISTS `Garages` (
`ID` int(11) NOT NULL,
  `MaisonID` int(11) NOT NULL,
  `PosX` int(11) NOT NULL,
  `PosY` int(11) NOT NULL,
  `PosZ` int(11) NOT NULL,
  `IDveh1` int(11) NOT NULL DEFAULT '-1',
  `IDveh2` int(11) NOT NULL DEFAULT '-1',
  `Locked` tinyint(1) NOT NULL DEFAULT '0'
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `Garages`
--

INSERT INTO `Garages` (`ID`, `MaisonID`, `PosX`, `PosY`, `PosZ`, `IDveh1`, `IDveh2`, `Locked`) VALUES
(1, 1, 103, -1957, 21, 0, 0, 0),
(2, 2, 85, -1973, 21, 0, 0, 0);

-- --------------------------------------------------------

--
-- Structure de la table `Inventaire`
--

CREATE TABLE IF NOT EXISTS `Inventaire` (
`ID` int(11) NOT NULL,
  `Type` int(11) DEFAULT NULL,
  `IDinventaire` int(11) DEFAULT NULL,
  `Item` int(11) DEFAULT NULL,
  `Nombre` int(11) DEFAULT NULL
) ENGINE=MyISAM AUTO_INCREMENT=42 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `Inventaire`
--

INSERT INTO `Inventaire` (`ID`, `Type`, `IDinventaire`, `Item`, `Nombre`) VALUES
(1, 1, 10, 1, 22),
(2, 1, 10, 2, 5),
(3, 1, 10, 3, 3),
(29, 1, 17, 8, 0),
(28, 1, 17, 7, 1),
(6, 1, 10, 5, 1),
(26, 1, 2, 9, 0),
(25, 1, 2, 10, 1),
(27, 1, 17, 6, 1),
(23, 1, 2, 11, 0),
(22, 1, 2, 6, 1),
(12, 1, 10, 6, 1),
(13, 1, 10, 7, 1),
(14, 1, 10, 8, 1),
(15, 1, 10, 10, 1),
(16, 1, 10, 9, 2),
(34, 1, 2, 16, 9),
(33, 1, 2, 4, 0),
(32, 1, 2, 15, 1),
(31, 1, 17, 9, 0),
(30, 1, 17, 10, 1),
(35, 1, 0, 16, 0),
(36, 1, 10, 16, 0),
(37, 1, 2, 17, 0),
(38, 1, 2, 19, 0),
(39, 1, 19, 5, 1),
(40, 1, 21, 5, 0),
(41, 1, 22, 5, 1);

-- --------------------------------------------------------

--
-- Structure de la table `Item`
--

CREATE TABLE IF NOT EXISTS `Item` (
`id` int(11) NOT NULL,
  `coffreid` int(11) NOT NULL,
  `type` int(11) NOT NULL,
  `nombre` int(11) NOT NULL,
  `data1` int(11) NOT NULL DEFAULT '0',
  `data2` int(11) NOT NULL DEFAULT '0',
  `data3` int(11) NOT NULL DEFAULT '0',
  `updatable` tinyint(4) NOT NULL DEFAULT '1'
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `Item`
--

INSERT INTO `Item` (`id`, `coffreid`, `type`, `nombre`, `data1`, `data2`, `data3`, `updatable`) VALUES
(1, 1, 1, 1, 0, 0, 0, 1),
(2, 1, 2, 1, 0, 0, 0, 1),
(3, 0, 2, 5, 0, 0, 0, 1),
(4, 0, 1, 2, 0, 0, 0, 1),
(5, 0, 4, 1, 0, 0, 0, 1),
(6, 1, 2, 5, 0, 0, 0, 1),
(7, 1, 1, 2, 0, 0, 0, 1),
(8, 1, 4, 1, 0, 0, 0, 1);

-- --------------------------------------------------------

--
-- Structure de la table `LogTextJoueur`
--

CREATE TABLE IF NOT EXISTS `LogTextJoueur` (
`ID` int(11) NOT NULL,
  `Type` text,
  `Joueur` text,
  `Message` text
) ENGINE=MyISAM AUTO_INCREMENT=1118 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `LogTextJoueur`
--

INSERT INTO `LogTextJoueur` (`ID`, `Type`, `Joueur`, `Message`) VALUES
(1, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(2, 'Message', 'Esteban_Alcaraz', 'lol'),
(3, 'Message', 'Esteban_Alcaraz', 'mdr'),
(4, 'Commande', 'Esteban_Alcaraz', '/giveweapon esteban assautrifle 800'),
(5, 'Commande', 'Esteban_Alcaraz', '/giveweapon esteban assaut 500'),
(6, 'Commande', 'Esteban_Alcaraz', '/giveweapon esteban assaultrifle 500'),
(7, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(8, 'Commande', 'Esteban_Alcaraz', '/addquestion'),
(9, 'Message', 'Esteban_Alcaraz', 'lol'),
(10, 'Message', 'Esteban_Alcaraz', 'lol'),
(11, 'Message', 'Esteban_Alcaraz', 'mdr'),
(12, 'Message', 'Esteban_Alcaraz', 'ptdr'),
(13, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(14, 'Commande', 'Esteban_Alcaraz', '/addquestion'),
(15, 'Message', 'Esteban_Alcaraz', 'Comment tu avances?'),
(16, 'Message', 'Esteban_Alcaraz', 'En accelerant'),
(17, 'Message', 'Esteban_Alcaraz', 'En freinant'),
(18, 'Message', 'Esteban_Alcaraz', 'En enculant des meres'),
(19, 'Commande', 'Esteban_Alcaraz', '/addquestion'),
(20, 'Message', 'Esteban_Alcaraz', 'Tu fais quoi a un feu rouge?'),
(21, 'Message', 'Esteban_Alcaraz', 'Tu avance'),
(22, 'Message', 'Esteban_Alcaraz', 'je sais pas'),
(23, 'Commande', 'Esteban_Alcaraz', '/addquestion'),
(24, 'Message', 'Esteban_Alcaraz', 'Tu fais quoi a un Stop?'),
(25, 'Message', 'Esteban_Alcaraz', 'Tu éteins le moteur'),
(26, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(27, 'Message', 'Esteban_Alcaraz', '1'),
(28, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule manchez'),
(29, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule shotaro'),
(30, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sanchez2'),
(31, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sanchez'),
(32, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule ratbike'),
(33, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule fagio'),
(34, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule faggio'),
(35, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule faggio2'),
(36, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule faggio3'),
(37, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule 884483972'),
(38, 'Commande', 'Esteban_Alcaraz', '/areanimer'),
(39, 'Commande', 'Esteban_Alcaraz', '/reanimer'),
(40, 'Commande', 'Esteban_Alcaraz', '/reanimer est'),
(41, 'Commande', 'Esteban_Alcaraz', '/lspd'),
(42, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(43, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(44, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(45, 'Message', 'Esteban_Alcaraz', '3'),
(46, 'Message', 'Esteban_Alcaraz', '3'),
(47, 'Message', 'Esteban_Alcaraz', '2'),
(48, 'Message', 'Esteban_Alcaraz', '1'),
(49, 'Message', 'Esteban_Alcaraz', '2'),
(50, 'Message', 'Esteban_Alcaraz', '1'),
(51, 'Message', 'Esteban_Alcaraz', '1'),
(52, 'Message', 'Esteban_Alcaraz', '1'),
(53, 'Message', 'Esteban_Alcaraz', '1'),
(54, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(55, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(56, 'Message', 'Esteban_Alcaraz', '2'),
(57, 'Message', 'Esteban_Alcaraz', '3'),
(58, 'Message', 'Esteban_Alcaraz', '1'),
(59, 'Message', 'Esteban_Alcaraz', '1'),
(60, 'Message', 'Esteban_Alcaraz', '1'),
(61, 'Message', 'Esteban_Alcaraz', '0'),
(62, 'Message', 'Esteban_Alcaraz', '1'),
(63, 'Message', 'Esteban_Alcaraz', '2'),
(64, 'Message', 'Esteban_Alcaraz', '3'),
(65, 'Message', 'Esteban_Alcaraz', '1'),
(66, 'Commande', 'Esteban_Alcaraz', '/ainvite'),
(67, 'Commande', 'Esteban_Alcaraz', '/ainvite esteban 1'),
(68, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(69, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(70, 'Message', 'Esteban_Alcaraz', '1'),
(71, 'Message', 'Esteban_Alcaraz', '2'),
(72, 'Message', 'Esteban_Alcaraz', '1'),
(73, 'Message', 'Esteban_Alcaraz', '3'),
(74, 'Message', 'Esteban_Alcaraz', '2'),
(75, 'Message', 'Esteban_Alcaraz', '1'),
(76, 'Message', 'Esteban_Alcaraz', '2'),
(77, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(78, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(79, 'Message', 'Esteban_Alcaraz', '3'),
(80, 'Message', 'Esteban_Alcaraz', '2'),
(81, 'Message', 'Esteban_Alcaraz', '3'),
(82, 'Message', 'Esteban_Alcaraz', '2'),
(83, 'Message', 'Esteban_Alcaraz', '2'),
(84, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(85, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(86, 'Message', 'Esteban_Alcaraz', '1'),
(87, 'Message', 'Esteban_Alcaraz', '3'),
(88, 'Message', 'Esteban_Alcaraz', '2'),
(89, 'Message', 'Esteban_Alcaraz', '3'),
(90, 'Message', 'Esteban_Alcaraz', '2'),
(91, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(92, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(93, 'Message', 'Esteban_Alcaraz', '2'),
(94, 'Message', 'Esteban_Alcaraz', '1'),
(95, 'Message', 'Esteban_Alcaraz', '2'),
(96, 'Message', 'Esteban_Alcaraz', '2'),
(97, 'Message', 'Esteban_Alcaraz', '2'),
(98, 'Message', 'Esteban_Alcaraz', '2'),
(99, 'Message', 'Esteban_Alcaraz', '1'),
(100, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(101, 'Commande', 'Esteban_Alcaraz', '/setessence 9 100'),
(102, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(103, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(104, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(105, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sanctus'),
(106, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(107, 'Commande', 'Esteban_Alcaraz', '/fc'),
(108, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(109, 'Commande', 'Esteban_Alcaraz', '/fc'),
(110, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(111, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule blista'),
(112, 'Commande', 'Esteban_Alcaraz', '/giveweapon WEAPON_ASSAULTRIFLE_MK2'),
(113, 'Commande', 'Esteban_Alcaraz', '/giveweapon esteban WEAPON_ASSAULTRIFLE_MK2 200'),
(114, 'Commande', 'Esteban_Alcaraz', '/giveweapon esteban 961495388 200'),
(115, 'Commande', 'Esteban_Alcaraz', '/giveweapon esteban 4208062921 200'),
(116, 'Commande', 'Esteban_Alcaraz', '/giveweapon esteban 4208062921'),
(117, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule blista2'),
(118, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule issi2'),
(119, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule panto'),
(120, 'Commande', 'Esteban_Alcaraz', '/goto travis'),
(121, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule enduro'),
(122, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule defilo'),
(123, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule defiler'),
(124, 'Commande', 'Esteban_Alcaraz', '/startgf modmenu'),
(125, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule avarus'),
(126, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule blazer4'),
(127, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule chimera'),
(128, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule esskey'),
(129, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule hakuchou2'),
(130, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule hakuchou'),
(131, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sanchez'),
(132, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sanchez2'),
(133, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(134, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(135, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(136, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(137, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(138, 'Message', 'Esteban_Alcaraz', '2'),
(139, 'Message', 'Esteban_Alcaraz', '2'),
(140, 'Message', 'Esteban_Alcaraz', '1'),
(141, 'Message', 'Esteban_Alcaraz', '1'),
(142, 'Message', 'Esteban_Alcaraz', '3'),
(143, 'Message', 'Esteban_Alcaraz', '3'),
(144, 'Message', 'Esteban_Alcaraz', '0'),
(145, 'Message', 'Esteban_Alcaraz', '0'),
(146, 'Message', 'Esteban_Alcaraz', '4'),
(147, 'Message', 'Esteban_Alcaraz', '4'),
(148, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(149, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(150, 'Message', 'Esteban_Alcaraz', '1'),
(151, 'Message', 'Esteban_Alcaraz', '1'),
(152, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(153, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(154, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(155, 'Message', 'Esteban_Alcaraz', '1'),
(156, 'Message', 'Esteban_Alcaraz', '1'),
(157, 'Message', 'Esteban_Alcaraz', '.'),
(158, 'Message', 'Esteban_Alcaraz', '.'),
(159, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(160, 'Message', 'Esteban_Alcaraz', '.'),
(161, 'Message', 'Esteban_Alcaraz', '.'),
(162, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(163, 'Message', 'Esteban_Alcaraz', '.'),
(164, 'Message', 'Esteban_Alcaraz', '.'),
(165, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(166, 'Message', 'Esteban_Alcaraz', '.'),
(167, 'Message', 'Esteban_Alcaraz', 'sqfsdf'),
(168, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(169, 'Message', 'Esteban_Alcaraz', '.'),
(170, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(171, 'Message', 'Esteban_Alcaraz', '.'),
(172, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(173, 'Message', 'Esteban_Alcaraz', '1'),
(174, 'Message', 'Esteban_Alcaraz', '0'),
(175, 'Message', 'Esteban_Alcaraz', '2'),
(176, 'Message', 'Esteban_Alcaraz', '2'),
(177, 'Message', 'Esteban_Alcaraz', '2'),
(178, 'Message', 'Esteban_Alcaraz', '2'),
(179, 'Message', 'Esteban_Alcaraz', '3'),
(180, 'Message', 'Esteban_Alcaraz', '2'),
(181, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(182, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(183, 'Message', 'Esteban_Alcaraz', '2'),
(184, 'Message', 'Esteban_Alcaraz', '2'),
(185, 'Message', 'Esteban_Alcaraz', '1'),
(186, 'Message', 'Esteban_Alcaraz', '1'),
(187, 'Message', 'Esteban_Alcaraz', '1'),
(188, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(189, 'Message', 'Esteban_Alcaraz', '1'),
(190, 'Message', 'Esteban_Alcaraz', '3'),
(191, 'Message', 'Esteban_Alcaraz', '3'),
(192, 'Message', 'Esteban_Alcaraz', '3'),
(193, 'Message', 'Esteban_Alcaraz', '1'),
(194, 'Message', 'Esteban_Alcaraz', '1'),
(195, 'Message', 'Esteban_Alcaraz', '1'),
(196, 'Message', 'Esteban_Alcaraz', '3'),
(197, 'Message', 'Esteban_Alcaraz', '3'),
(198, 'Message', 'Esteban_Alcaraz', '3'),
(199, 'Message', 'Esteban_Alcaraz', '3'),
(200, 'Message', 'Esteban_Alcaraz', '2'),
(201, 'Message', 'Esteban_Alcaraz', '2'),
(202, 'Message', 'Esteban_Alcaraz', '2'),
(203, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(204, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(205, 'Message', 'Esteban_Alcaraz', '1'),
(206, 'Message', 'Esteban_Alcaraz', '3'),
(207, 'Message', 'Esteban_Alcaraz', '3'),
(208, 'Message', 'Esteban_Alcaraz', '1'),
(209, 'Message', 'Esteban_Alcaraz', '1'),
(210, 'Message', 'Esteban_Alcaraz', '3'),
(211, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(212, 'Message', 'Esteban_Alcaraz', '2'),
(213, 'Message', 'Esteban_Alcaraz', '2'),
(214, 'Message', 'Esteban_Alcaraz', '2'),
(215, 'Message', 'Esteban_Alcaraz', '2'),
(216, 'Message', 'Esteban_Alcaraz', '2'),
(217, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(218, 'Commande', 'Esteban_Alcaraz', '/sethp esteban 100'),
(219, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(220, 'Message', 'Esteban_Alcaraz', '3'),
(221, 'Message', 'Esteban_Alcaraz', '1'),
(222, 'Message', 'Esteban_Alcaraz', '1'),
(223, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(224, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(225, 'Message', 'Esteban_Alcaraz', '1'),
(226, 'Message', 'Esteban_Alcaraz', '1'),
(227, 'Message', 'Esteban_Alcaraz', '1'),
(228, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(229, 'Commande', 'Esteban_Alcaraz', '/testquestion'),
(230, 'Message', 'Esteban_Alcaraz', '3'),
(231, 'Message', 'Esteban_Alcaraz', '2'),
(232, 'Message', 'Esteban_Alcaraz', '2'),
(233, 'Message', 'Esteban_Alcaraz', 'Lol'),
(234, 'Message', 'Esteban_Alcaraz', '1'),
(235, 'Message', 'Esteban_Alcaraz', '2'),
(236, 'Message', 'Esteban_Alcaraz', '3'),
(237, 'Commande', 'Esteban_Alcaraz', '/gotocar 25'),
(238, 'Commande', 'Esteban_Alcaraz', '/getincar 25'),
(239, 'Commande', 'Esteban_Alcaraz', '/vr 25'),
(240, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(241, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(242, 'Commande', 'Esteban_Alcaraz', '/vr 25'),
(243, 'Commande', 'Esteban_Alcaraz', '/creegarage'),
(244, 'Commande', 'Esteban_Alcaraz', '/creergarage'),
(245, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(246, 'Commande', 'Esteban_Alcaraz', '/creergarage 1'),
(247, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(248, 'Commande', 'Esteban_Alcaraz', '/getcar 25'),
(249, 'Commande', 'Ange_Graziani', '/§Fc'),
(250, 'Commande', 'Ange_Graziani', '/fc'),
(251, 'Commande', 'Ange_Graziani', '/fcoff'),
(252, 'Commande', 'Ange_Graziani', '/Fc'),
(253, 'Commande', 'Ange_Graziani', '/fcoff'),
(254, 'Commande', 'Esteban_Alcaraz', '/lspd'),
(255, 'Commande', 'Esteban_Alcaraz', '/fc'),
(256, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(257, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(258, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(259, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(260, 'Commande', 'Esteban_Alcaraz', '/fc'),
(261, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(262, 'Commande', 'Esteban_Alcaraz', '/gotocar 28'),
(263, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(264, 'Commande', 'Esteban_Alcaraz', '/lspd'),
(265, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(266, 'Commande', 'Esteban_Alcaraz', '/setessence 17 100'),
(267, 'Commande', 'Esteban_Alcaraz', '/startgf modmenu'),
(268, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(269, 'Commande', 'Esteban_Alcaraz', '/vr 17'),
(270, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sanctus'),
(271, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(272, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(273, 'Commande', 'Esteban_Alcaraz', '/vr 34'),
(274, 'Commande', 'Esteban_Alcaraz', '/addpompe'),
(275, 'Commande', 'Esteban_Alcaraz', '/addpompes'),
(276, 'Commande', 'Esteban_Alcaraz', '/creerpompe'),
(277, 'Commande', 'Esteban_Alcaraz', '/creerpompes'),
(278, 'Commande', 'Esteban_Alcaraz', '/creerpompessence'),
(279, 'Commande', 'Esteban_Alcaraz', '/creerpompessences'),
(280, 'Commande', 'Esteban_Alcaraz', '/creerpompeessence'),
(281, 'Commande', 'Esteban_Alcaraz', '/creerpompeessence 1'),
(282, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sanctus'),
(283, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(284, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sanctus'),
(285, 'Commande', 'Esteban_Alcaraz', '/setessence 34 0'),
(286, 'Commande', 'Ange_Graziani', '/spawnvehicule pfister811'),
(287, 'Commande', 'Ange_Graziani', '/'),
(288, 'Commande', 'Ange_Graziani', '/fc'),
(289, 'Commande', 'Ange_Graziani', '/fcoff'),
(290, 'Commande', 'Ange_Graziani', '/setessence'),
(291, 'Commande', 'Ange_Graziani', '/setessence'),
(292, 'Commande', 'Ange_Graziani', '/dl'),
(293, 'Commande', 'Ange_Graziani', '/dl true'),
(294, 'Commande', 'Ange_Graziani', '/dl false'),
(295, 'Commande', 'Ange_Graziani', '/setessence 35 100'),
(296, 'Commande', 'Ange_Graziani', '/setessence 35 100'),
(297, 'Commande', 'Ange_Graziani', '/spawnvehicule trashmaster'),
(298, 'Commande', 'Ange_Graziani', '/spawnvehicule trash'),
(299, 'Commande', 'Ange_Graziani', '/spawnvehicule trash2'),
(300, 'Commande', 'Ange_Graziani', '/POS'),
(301, 'Commande', 'Ange_Graziani', '/rot'),
(302, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(303, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sanctus'),
(304, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(305, 'Commande', 'Esteban_Alcaraz', '/setessence'),
(306, 'Commande', 'Esteban_Alcaraz', '/setessence 34 50'),
(307, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(308, 'Commande', 'Esteban_Alcaraz', '/sethp esteban 100'),
(309, 'Commande', 'Esteban_Alcaraz', '/getincar 34'),
(310, 'Commande', 'Esteban_Alcaraz', '/getincar 34'),
(311, 'Commande', 'Esteban_Alcaraz', '/vr 34'),
(312, 'Commande', 'Esteban_Alcaraz', '/vr 34'),
(313, 'Commande', 'Esteban_Alcaraz', '/sethp esteban 100'),
(314, 'Commande', 'Esteban_Alcaraz', '/vr 34'),
(315, 'Commande', 'Esteban_Alcaraz', '/vr 34'),
(316, 'Commande', 'Esteban_Alcaraz', '/vr 34'),
(317, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(318, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sanctus'),
(319, 'Commande', 'Esteban_Alcaraz', '/setessence 34 50'),
(320, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(321, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sanctud'),
(322, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sanctus'),
(323, 'Commande', 'Esteban_Alcaraz', '/setessence 34 50'),
(324, 'Commande', 'Esteban_Alcaraz', '/setjob'),
(325, 'Commande', 'Esteban_Alcaraz', '/jivnite'),
(326, 'Commande', 'Esteban_Alcaraz', '/jinvite'),
(327, 'Commande', 'Esteban_Alcaraz', '/job'),
(328, 'Commande', 'Esteban_Alcaraz', '/convoyeur'),
(329, 'Commande', 'Esteban_Alcaraz', '/ainvitejoib'),
(330, 'Commande', 'Esteban_Alcaraz', '/ainvitejob'),
(331, 'Commande', 'Esteban_Alcaraz', '/ainvitejob esteban 1'),
(332, 'Commande', 'Esteban_Alcaraz', '/fin'),
(333, 'Commande', 'Esteban_Alcaraz', '/finir'),
(334, 'Commande', 'Esteban_Alcaraz', '/finirjob'),
(335, 'Commande', 'Esteban_Alcaraz', '/finirconv'),
(336, 'Commande', 'Esteban_Alcaraz', '/stopjob'),
(337, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(338, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(339, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(340, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(341, 'Commande', 'Esteban_Alcaraz', '/setessence 2 100'),
(342, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(343, 'Commande', 'Esteban_Alcaraz', '/Stopjob'),
(344, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(345, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(346, 'Commande', 'Esteban_Alcaraz', '/gotocar 25'),
(347, 'Commande', 'Esteban_Alcaraz', '/save'),
(348, 'Commande', 'Esteban_Alcaraz', '/save NewPosExtAutoEcole'),
(349, 'Commande', 'Esteban_Alcaraz', '/save PosMenuAutoEcole'),
(350, 'Commande', 'Esteban_Alcaraz', '/vr 25'),
(351, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(352, 'Message', 'Esteban_Alcaraz', '1'),
(353, 'Message', 'Esteban_Alcaraz', '1'),
(354, 'Message', 'Esteban_Alcaraz', '1'),
(355, 'Message', 'Esteban_Alcaraz', '1'),
(356, 'Message', 'Esteban_Alcaraz', '0'),
(357, 'Message', 'Esteban_Alcaraz', '0'),
(358, 'Message', 'Esteban_Alcaraz', '2'),
(359, 'Message', 'Esteban_Alcaraz', '2'),
(360, 'Message', 'Esteban_Alcaraz', '3'),
(361, 'Message', 'Esteban_Alcaraz', '3'),
(362, 'Message', 'Esteban_Alcaraz', '1'),
(363, 'Message', 'Esteban_Alcaraz', '1'),
(364, 'Message', 'Esteban_Alcaraz', '2'),
(365, 'Message', 'Esteban_Alcaraz', '2'),
(366, 'Message', 'Esteban_Alcaraz', '3'),
(367, 'Message', 'Esteban_Alcaraz', '3'),
(368, 'Message', 'Esteban_Alcaraz', 'sdfg'),
(369, 'Message', 'Esteban_Alcaraz', 'sdfg'),
(370, 'Message', 'Esteban_Alcaraz', '<sdf'),
(371, 'Message', 'Esteban_Alcaraz', '<sdf'),
(372, 'Message', 'Esteban_Alcaraz', 'mpm'),
(373, 'Message', 'Esteban_Alcaraz', 'mpm'),
(374, 'Commande', 'Esteban_Alcaraz', '/kick'),
(375, 'Commande', 'Esteban_Alcaraz', '/kick travis /'),
(376, 'Commande', 'Esteban_Alcaraz', '/goto travis'),
(377, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(378, 'Commande', 'Esteban_Alcaraz', '/fc'),
(379, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(380, 'Message', 'Esteban_Alcaraz', 'sdg'),
(381, 'Message', 'Esteban_Alcaraz', 'dfsdfg'),
(382, 'Message', 'Esteban_Alcaraz', '1'),
(383, 'Message', 'Esteban_Alcaraz', '3'),
(384, 'Message', 'Esteban_Alcaraz', '1'),
(385, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(386, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sanctus'),
(387, 'Commande', 'Esteban_Alcaraz', '/startgf Interiors'),
(388, 'Commande', 'Esteban_Alcaraz', '/interiors'),
(389, 'Commande', 'Esteban_Alcaraz', '/interiors'),
(390, 'Commande', 'Esteban_Alcaraz', '/interiors'),
(391, 'Commande', 'Esteban_Alcaraz', '/interiors'),
(392, 'Commande', 'Esteban_Alcaraz', '/interiors'),
(393, 'Commande', 'Esteban_Alcaraz', '/interiors'),
(394, 'Commande', 'Esteban_Alcaraz', '/interiors'),
(395, 'Commande', 'Esteban_Alcaraz', '/interiors'),
(396, 'Commande', 'Esteban_Alcaraz', '/interiors'),
(397, 'Commande', 'Esteban_Alcaraz', '/interiors'),
(398, 'Commande', 'Esteban_Alcaraz', '/interiors'),
(399, 'Commande', 'Esteban_Alcaraz', '/interiors'),
(400, 'Commande', 'Esteban_Alcaraz', '/interiors'),
(401, 'Commande', 'Esteban_Alcaraz', '/interiors'),
(402, 'Commande', 'Esteban_Alcaraz', '/interiors'),
(403, 'Commande', 'Esteban_Alcaraz', '/interiors'),
(404, 'Commande', 'Esteban_Alcaraz', '/interiors'),
(405, 'Commande', 'Esteban_Alcaraz', '/interiors'),
(406, 'Commande', 'Esteban_Alcaraz', '/gotocar 25'),
(407, 'Commande', 'Esteban_Alcaraz', '/save posspawnvehiculeae'),
(408, 'Commande', 'Esteban_Alcaraz', '/rot'),
(409, 'Commande', 'Esteban_Alcaraz', '/pos'),
(410, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(411, 'Commande', 'Esteban_Alcaraz', '/setessence 34 100'),
(412, 'Commande', 'Esteban_Alcaraz', '/addcheckpointautoecole'),
(413, 'Commande', 'Esteban_Alcaraz', '/addcheckpointautoecole'),
(414, 'Commande', 'Esteban_Alcaraz', '/addcheckpointautoecole'),
(415, 'Commande', 'Esteban_Alcaraz', '/addcheckpointautoecole'),
(416, 'Commande', 'Esteban_Alcaraz', '/addcheckpointautoecole'),
(417, 'Commande', 'Esteban_Alcaraz', '/resetcheckpointautoecole'),
(418, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(419, 'Commande', 'Esteban_Alcaraz', '/resetcheckpointautoecole'),
(420, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sanctus'),
(421, 'Commande', 'Esteban_Alcaraz', '/addcheckpointautoecole'),
(422, 'Commande', 'Esteban_Alcaraz', '/resetcheckpointautoecole'),
(423, 'Commande', 'Esteban_Alcaraz', '/setessence 34 100'),
(424, 'Commande', 'Esteban_Alcaraz', '/addcheckpointautoecole'),
(425, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(426, 'Commande', 'Esteban_Alcaraz', '/Stopgf'),
(427, 'Commande', 'Esteban_Alcaraz', '/Stop'),
(428, 'Commande', 'Esteban_Alcaraz', '/fc'),
(429, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(430, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(431, 'Commande', 'Esteban_Alcaraz', '/fc'),
(432, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(433, 'Commande', 'Esteban_Alcaraz', '/addcheckpointautoecole'),
(434, 'Commande', 'Esteban_Alcaraz', '/addcheckpointautoecole'),
(435, 'Commande', 'Esteban_Alcaraz', '/addcheckpointautoecole'),
(436, 'Commande', 'Esteban_Alcaraz', '/addcheckpointautoecole'),
(437, 'Commande', 'Esteban_Alcaraz', '/addcheckpointautoecole'),
(438, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(439, 'Commande', 'Esteban_Alcaraz', '/fc'),
(440, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(441, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sanctus'),
(442, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(443, 'Commande', 'Esteban_Alcaraz', '/testson'),
(444, 'Commande', 'Esteban_Alcaraz', '/testson'),
(445, 'Commande', 'Esteban_Alcaraz', '/testson DOOR_BUZZ MP_PLAYER_APARTMENT'),
(446, 'Commande', 'Esteban_Alcaraz', '/testson DOOR_BUZZ MP_PLAYER_APARTMENT'),
(447, 'Commande', 'Esteban_Alcaraz', '/testson Door_Open DOCKS_HEIST_FINALE_2B_SOUNDS'),
(448, 'Commande', 'Esteban_Alcaraz', '/testson Door_Open DOCKS_HEIST_FINALE_2B_SOUNDS'),
(449, 'Commande', 'Esteban_Alcaraz', '/testson Door_Open DOCKS_HEIST_FINALE_2B_SOUNDS'),
(450, 'Commande', 'Esteban_Alcaraz', '/testson FINDING_VIRUS LESTER1A_SOUNDS'),
(451, 'Commande', 'Esteban_Alcaraz', '/testson SAFE_DOOR_OPEN SAFE_CRACK_SOUNDSET'),
(452, 'Commande', 'Esteban_Alcaraz', '/testson OPENING DOOR_GARAGE'),
(453, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(454, 'Commande', 'Esteban_Alcaraz', '/testson'),
(455, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(456, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule prototipo'),
(457, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule'),
(458, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule'),
(459, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule'),
(460, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(461, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule prototipo'),
(462, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule'),
(463, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(464, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule prototipo'),
(465, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(466, 'Commande', 'Esteban_Alcaraz', '/testson'),
(467, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule'),
(468, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(469, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(470, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(471, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule prototipo'),
(472, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule'),
(473, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(474, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(475, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(476, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule prototipo'),
(477, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(478, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(479, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule cheetah'),
(480, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule cheetah2'),
(481, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(482, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(483, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(484, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(485, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(486, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(487, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(488, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(489, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(490, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule blista'),
(491, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(492, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(493, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(494, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(495, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(496, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule blista'),
(497, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(498, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(499, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(500, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(501, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(502, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(503, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(504, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(505, 'Commande', 'Esteban_Alcaraz', '/gotocar 25'),
(506, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 25'),
(507, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 25'),
(508, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(509, 'Commande', 'Esteban_Alcaraz', '/getincar 25'),
(510, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 25'),
(511, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 25'),
(512, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 25'),
(513, 'Commande', 'Esteban_Alcaraz', '/fc'),
(514, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(515, 'Commande', 'Esteban_Alcaraz', '/gotocar 25'),
(516, 'Commande', 'Esteban_Alcaraz', '/fc'),
(517, 'Commande', 'Esteban_Alcaraz', '/fc'),
(518, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(519, 'Commande', 'Esteban_Alcaraz', '/getincar 25'),
(520, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 25'),
(521, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 25'),
(522, 'Commande', 'Esteban_Alcaraz', '/gotocar 25'),
(523, 'Commande', 'Esteban_Alcaraz', '/getincar 25'),
(524, 'Commande', 'Esteban_Alcaraz', '/setessence 25 100'),
(525, 'Commande', 'Esteban_Alcaraz', '/vr 25'),
(526, 'Commande', 'Esteban_Alcaraz', '/setmoney'),
(527, 'Commande', 'Esteban_Alcaraz', '/vr 25'),
(528, 'Commande', 'Esteban_Alcaraz', '/dl'),
(529, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(530, 'Commande', 'Esteban_Alcaraz', '/setessence 23 100'),
(531, 'Commande', 'Esteban_Alcaraz', '/setessence 22 100'),
(532, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(533, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(534, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(535, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(536, 'Commande', 'Esteban_Alcaraz', '/setessence 9'),
(537, 'Commande', 'Esteban_Alcaraz', '/setessence 9 100'),
(538, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(539, 'Commande', 'Esteban_Alcaraz', '/fc'),
(540, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(541, 'Commande', 'Esteban_Alcaraz', '/vr 25'),
(542, 'Commande', 'Esteban_Alcaraz', '/v preterclef'),
(543, 'Commande', 'Esteban_Alcaraz', '/preterclef'),
(544, 'Commande', 'Esteban_Alcaraz', '/vpreterclef'),
(545, 'Commande', 'Esteban_Alcaraz', '/donner'),
(546, 'Commande', 'Esteban_Alcaraz', '/donnerclef'),
(547, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule limo'),
(548, 'Commande', 'Esteban_Alcaraz', '/setadminlevel'),
(549, 'Commande', 'Esteban_Alcaraz', '/setadminlvl'),
(550, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule Stretch'),
(551, 'Commande', 'Esteban_Alcaraz', '/startgf modmenu'),
(552, 'Commande', 'Esteban_Alcaraz', '/giveweapon,'),
(553, 'Commande', 'Esteban_Alcaraz', '/giveweapon'),
(554, 'Commande', 'Esteban_Alcaraz', '/giveweapon esteban pistol50 500'),
(555, 'Commande', 'Esteban_Alcaraz', '/c'),
(556, 'Commande', 'Esteban_Alcaraz', '/chu'),
(557, 'Commande', 'Esteban_Alcaraz', '/chu Allo?'),
(558, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(559, 'Commande', 'Esteban_Alcaraz', '/vr 34'),
(560, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(561, 'Commande', 'Esteban_Alcaraz', '/testsonvehicule 34'),
(562, 'Commande', 'Esteban_Alcaraz', '/goto wende'),
(563, 'Message', 'Esteban_Alcaraz', 'ALlo?'),
(564, 'Commande', 'Esteban_Alcaraz', '/goto 1'),
(565, 'Commande', 'Esteban_Alcaraz', '/goto wende'),
(566, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(567, 'Commande', 'Esteban_Alcaraz', '/setessence 28 100'),
(568, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(569, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(570, 'Commande', 'Esteban_Alcaraz', '/fc'),
(571, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(572, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(573, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(574, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(575, 'Commande', 'Esteban_Alcaraz', '/fc'),
(576, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(577, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(578, 'Commande', 'Esteban_Alcaraz', '/fc'),
(579, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(580, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(581, 'Commande', 'Esteban_Alcaraz', '/fc'),
(582, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(583, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(584, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(585, 'Commande', 'Esteban_Alcaraz', '/setessence 9'),
(586, 'Commande', 'Esteban_Alcaraz', '/setessence 9 100'),
(587, 'Commande', 'Esteban_Alcaraz', '/vr 9'),
(588, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(589, 'Commande', 'Esteban_Alcaraz', '/vr 9'),
(590, 'Commande', 'Esteban_Alcaraz', '/vr 9'),
(591, 'Commande', 'Esteban_Alcaraz', '/startgf'),
(592, 'Commande', 'Esteban_Alcaraz', '/startgf modmenu'),
(593, 'Commande', 'Ange_Graziani', '/pos'),
(594, 'Commande', 'Ange_Graziani', '/sethp ange 100'),
(595, 'Commande', 'Esteban_Alcaraz', '/fc'),
(596, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(597, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(598, 'Commande', 'Esteban_Alcaraz', '/lspd'),
(599, 'Message', 'Esteban_Alcaraz', ':b'),
(600, 'Commande', 'Esteban_Alcaraz', '/b'),
(601, 'Commande', 'Esteban_Alcaraz', '/b'),
(602, 'Commande', 'Esteban_Alcaraz', '/b'),
(603, 'Commande', 'Esteban_Alcaraz', '/bc'),
(604, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(605, 'Commande', 'Esteban_Alcaraz', '/b'),
(606, 'Commande', 'Esteban_Alcaraz', '/b'),
(607, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(608, 'Message', 'Esteban_Alcaraz', ':b'),
(609, 'Commande', 'Esteban_Alcaraz', '/b'),
(610, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(611, 'Commande', 'Esteban_Alcaraz', '/b'),
(612, 'Commande', 'Esteban_Alcaraz', '/ainvite'),
(613, 'Commande', 'Esteban_Alcaraz', '/ainvite esteban 1'),
(614, 'Commande', 'Esteban_Alcaraz', '/b'),
(615, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(616, 'Commande', 'Esteban_Alcaraz', '/b'),
(617, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(618, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(619, 'Commande', 'Esteban_Alcaraz', '/kick polidoor'),
(620, 'Commande', 'Esteban_Alcaraz', '/kick polidoor .'),
(621, 'Commande', 'Esteban_Alcaraz', '/fc'),
(622, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(623, 'Commande', 'Esteban_Alcaraz', '/lspd'),
(624, 'Commande', 'Esteban_Alcaraz', '/b'),
(625, 'Commande', 'Esteban_Alcaraz', '/b'),
(626, 'Commande', 'Esteban_Alcaraz', '/b'),
(627, 'Commande', 'Esteban_Alcaraz', '/bc'),
(628, 'Commande', 'Esteban_Alcaraz', '/b'),
(629, 'Commande', 'Esteban_Alcaraz', '/bc'),
(630, 'Commande', 'Esteban_Alcaraz', '/b'),
(631, 'Commande', 'Esteban_Alcaraz', '/bc'),
(632, 'Commande', 'Esteban_Alcaraz', '/gotocar 38'),
(633, 'Commande', 'Esteban_Alcaraz', '/gotocar 32'),
(634, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule stretch'),
(635, 'Commande', 'Esteban_Alcaraz', '/startgf modmenu'),
(636, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(637, 'Commande', 'Esteban_Alcaraz', '/fc'),
(638, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(639, 'Commande', 'Esteban_Alcaraz', '/goto aar'),
(640, 'Commande', 'Esteban_Alcaraz', '/goto Aaron'),
(641, 'Commande', 'Esteban_Alcaraz', '/fc'),
(642, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(643, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule prototipo'),
(644, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule prototipo'),
(645, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule'),
(646, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule prototipo'),
(647, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule stretch'),
(648, 'Commande', 'Esteban_Alcaraz', '/sethp esteban 100'),
(649, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(650, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(651, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule prototipo'),
(652, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(653, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(654, 'Commande', 'Esteban_Alcaraz', '/sethp'),
(655, 'Commande', 'Esteban_Alcaraz', '/sethp aaron 100'),
(656, 'Commande', 'Esteban_Alcaraz', '/sethp estab 100'),
(657, 'Commande', 'Esteban_Alcaraz', '/sethp este 100'),
(658, 'Commande', 'Esteban_Alcaraz', '/ainvite'),
(659, 'Commande', 'Esteban_Alcaraz', '/ainvite aaron 1'),
(660, 'Commande', 'Esteban_Alcaraz', '/b'),
(661, 'Commande', 'Esteban_Alcaraz', '/bc'),
(662, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(663, 'Commande', 'Esteban_Alcaraz', '/setessence 19 100'),
(664, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(665, 'Commande', 'Esteban_Alcaraz', '/getincar 19'),
(666, 'Commande', 'Esteban_Alcaraz', '/giveweapon aaron RPG'),
(667, 'Commande', 'Esteban_Alcaraz', '/giveweapon aaron RPG 100'),
(668, 'Commande', 'Esteban_Alcaraz', '/getcar 19'),
(669, 'Commande', 'Esteban_Alcaraz', '/vr 19'),
(670, 'Commande', 'Esteban_Alcaraz', '/creerunité'),
(671, 'Commande', 'Aaron_Iverson', '/rejoindre unité 1'),
(672, 'Commande', 'Aaron_Iverson', '/rejoindreunité 1'),
(673, 'Commande', 'Aaron_Iverson', '/rejoindreunité 1'),
(674, 'Commande', 'Esteban_Alcaraz', '/rejoindreunité'),
(675, 'Commande', 'Aaron_Iverson', '/rejoindreunité1'),
(676, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(677, 'Commande', 'Esteban_Alcaraz', '/lspd'),
(678, 'Commande', 'Esteban_Alcaraz', '/b'),
(679, 'Commande', 'Esteban_Alcaraz', '/bc'),
(680, 'Commande', 'Esteban_Alcaraz', '/rejoindreunite'),
(681, 'Commande', 'Esteban_Alcaraz', '/gethere'),
(682, 'Commande', 'Esteban_Alcaraz', '/gethere aar'),
(683, 'Commande', 'Esteban_Alcaraz', '/b'),
(684, 'Commande', 'Esteban_Alcaraz', '/bc'),
(685, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(686, 'Commande', 'Esteban_Alcaraz', '/setessence 12 100'),
(687, 'Commande', 'Aaron_Iverson', '/creerunite'),
(688, 'Commande', 'Esteban_Alcaraz', '/w'),
(689, 'Message', 'Esteban_Alcaraz', 'Test;'),
(690, 'Commande', 'Esteban_Alcaraz', '/rejoindreuniute'),
(691, 'Commande', 'Esteban_Alcaraz', '/rejoindreunité'),
(692, 'Commande', 'Esteban_Alcaraz', '/rejoindreunite'),
(693, 'Commande', 'Esteban_Alcaraz', '/rejoindreunite 1'),
(694, 'Commande', 'Esteban_Alcaraz', '/r Test'),
(695, 'Commande', 'Aaron_Iverson', '/r met une phrase'),
(696, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(697, 'Commande', 'Aaron_Iverson', '/lspd'),
(698, 'Commande', 'Esteban_Alcaraz', '/lspd'),
(699, 'Commande', 'Esteban_Alcaraz', '/gethere aar'),
(700, 'Commande', 'Esteban_Alcaraz', '/gotocar 12'),
(701, 'Commande', 'Esteban_Alcaraz', '/gethere aart'),
(702, 'Commande', 'Esteban_Alcaraz', '/gethere aar'),
(703, 'Commande', 'Esteban_Alcaraz', '/ainvitejob'),
(704, 'Commande', 'Esteban_Alcaraz', '/ainvitejob aaron 1'),
(705, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(706, 'Commande', 'Esteban_Alcaraz', '/setessence 2 100'),
(707, 'Commande', 'Esteban_Alcaraz', '/setessence 1 100'),
(708, 'Commande', 'Esteban_Alcaraz', '/setadminleve'),
(709, 'Commande', 'Esteban_Alcaraz', '/setadminlevel'),
(710, 'Commande', 'Esteban_Alcaraz', '/setadminlevel aar 1'),
(711, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(712, 'Commande', 'Aaron_Iverson', '/stopjob'),
(713, 'Commande', 'Esteban_Alcaraz', '/stopjob'),
(714, 'Commande', 'Esteban_Alcaraz', '/rembourser'),
(715, 'Commande', 'Esteban_Alcaraz', '/payer'),
(716, 'Commande', 'Esteban_Alcaraz', '/payer aar 50000'),
(717, 'Commande', 'Esteban_Alcaraz', '/canonscier'),
(718, 'Commande', 'Esteban_Alcaraz', '/scier'),
(719, 'Commande', 'Esteban_Alcaraz', '/couper'),
(720, 'Commande', 'Aaron_Iverson', '/traficerarme'),
(721, 'Commande', 'Esteban_Alcaraz', '/trafiquerarme'),
(722, 'Commande', 'Aaron_Iverson', '/trafiquerarme'),
(723, 'Commande', 'Esteban_Alcaraz', '/areanimer'),
(724, 'Commande', 'Esteban_Alcaraz', '/areanimer aa'),
(725, 'Commande', 'Esteban_Alcaraz', '/setessence 12 100'),
(726, 'Commande', 'Esteban_Alcaraz', '/areanimer este'),
(727, 'Commande', 'Esteban_Alcaraz', '/areanimer aa'),
(728, 'Commande', 'Esteban_Alcaraz', '/giveweapon este pistol50 500'),
(729, 'Commande', 'Esteban_Alcaraz', '/areanimer aa'),
(730, 'Commande', 'Esteban_Alcaraz', '/giveweapon aar pistol50 100'),
(731, 'Commande', 'Esteban_Alcaraz', '/sethp este 100'),
(732, 'Commande', 'Esteban_Alcaraz', '/areanimer este'),
(733, 'Commande', 'Esteban_Alcaraz', '/areanimer este'),
(734, 'Commande', 'Esteban_Alcaraz', '/areanimer aa'),
(735, 'Commande', 'Esteban_Alcaraz', '/acceptermort'),
(736, 'Commande', 'Esteban_Alcaraz', '/goto aa'),
(737, 'Commande', 'Esteban_Alcaraz', '/startgf modmenu'),
(738, 'Commande', 'Esteban_Alcaraz', '/areanimer aa'),
(739, 'Commande', 'Esteban_Alcaraz', '/vr 12'),
(740, 'Commande', 'Esteban_Alcaraz', '/vr 12'),
(741, 'Commande', 'Esteban_Alcaraz', '/areanimer este'),
(742, 'Commande', 'Esteban_Alcaraz', '/goto aa'),
(743, 'Message', 'Esteban_Alcaraz', '.'),
(744, 'Message', 'Esteban_Alcaraz', '.'),
(745, 'Message', 'Aaron_Iverson', 'zdzad'),
(746, 'Message', 'Aaron_Iverson', 'zdzad'),
(747, 'Commande', 'Esteban_Alcaraz', '/debug'),
(748, 'Message', 'Esteban_Alcaraz', 'L'),
(749, 'Message', 'Esteban_Alcaraz', 'L'),
(750, 'Message', 'Aaron_Iverson', 'ff'),
(751, 'Message', 'Aaron_Iverson', 'ff'),
(752, 'Commande', 'Esteban_Alcaraz', '/debug'),
(753, 'Commande', 'Esteban_Alcaraz', '/areanimer aa'),
(754, 'Commande', 'Esteban_Alcaraz', '/debug'),
(755, 'Commande', 'Esteban_Alcaraz', '/debug'),
(756, 'Message', 'Esteban_Alcaraz', 'lol'),
(757, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(758, 'Message', 'Esteban_Alcaraz', 'lol'),
(759, 'Commande', 'Esteban_Alcaraz', '/debug'),
(760, 'Commande', 'Esteban_Alcaraz', '/setessence 12 100'),
(761, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(762, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(763, 'Commande', 'Esteban_Alcaraz', '/getincar 8'),
(764, 'Commande', 'Esteban_Alcaraz', '/getincar 9'),
(765, 'Commande', 'Esteban_Alcaraz', '/setessence 9 100'),
(766, 'Commande', 'Esteban_Alcaraz', '/setessence 6 100'),
(767, 'Commande', 'Esteban_Alcaraz', '/setessence 7 100'),
(768, 'Commande', 'Esteban_Alcaraz', '/getincar 14'),
(769, 'Commande', 'Esteban_Alcaraz', '/setessence 14 100'),
(770, 'Commande', 'Esteban_Alcaraz', '/setessence 15 100'),
(771, 'Commande', 'Esteban_Alcaraz', '/setessence 13 100'),
(772, 'Commande', 'Esteban_Alcaraz', '/setessence 19 100'),
(773, 'Commande', 'Esteban_Alcaraz', '/gotocar 18'),
(774, 'Commande', 'Esteban_Alcaraz', '/getincar 19'),
(775, 'Commande', 'Esteban_Alcaraz', '/getincar 17'),
(776, 'Commande', 'Esteban_Alcaraz', '/gotocar 18'),
(777, 'Commande', 'Esteban_Alcaraz', '/getcar 17'),
(778, 'Commande', 'Esteban_Alcaraz', '/setessence 17 100'),
(779, 'Commande', 'Esteban_Alcaraz', '/getincar 16'),
(780, 'Commande', 'Esteban_Alcaraz', '/setessence 16 100'),
(781, 'Commande', 'Esteban_Alcaraz', '/getincar 15'),
(782, 'Commande', 'Esteban_Alcaraz', '/fc'),
(783, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(784, 'Commande', 'Esteban_Alcaraz', '/lspd'),
(785, 'Commande', 'Esteban_Alcaraz', '/getincar 20'),
(786, 'Commande', 'Esteban_Alcaraz', '/setessence 20 100'),
(787, 'Commande', 'Esteban_Alcaraz', '/getincar 21'),
(788, 'Commande', 'Esteban_Alcaraz', '/setessence 21 100'),
(789, 'Commande', 'Esteban_Alcaraz', '/vr 21'),
(790, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(791, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(792, 'Commande', 'Esteban_Alcaraz', '/fc'),
(793, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(794, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(795, 'Commande', 'Aaron_Iverson', '/STARTGF modmenu'),
(796, 'Commande', 'Aaron_Iverson', '/b'),
(797, 'Commande', 'Aaron_Iverson', '/BC'),
(798, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(799, 'Commande', 'Esteban_Alcaraz', '/gethere aa'),
(800, 'Message', 'Esteban_Alcaraz', 'lol'),
(801, 'Message', 'Esteban_Alcaraz', 'lol'),
(802, 'Message', 'Aaron_Iverson', 'S	al'),
(803, 'Message', 'Esteban_Alcaraz', 'lol'),
(804, 'Message', 'Esteban_Alcaraz', 'lol'),
(805, 'Message', 'Esteban_Alcaraz', 'lol'),
(806, 'Message', 'Esteban_Alcaraz', 'lol'),
(807, 'Message', 'Esteban_Alcaraz', 'lol'),
(808, 'Message', 'Esteban_Alcaraz', 'lol'),
(809, 'Message', 'Esteban_Alcaraz', 'lol'),
(810, 'Message', 'Esteban_Alcaraz', 'lol'),
(811, 'Message', 'Esteban_Alcaraz', 'lol'),
(812, 'Commande', 'Esteban_Alcaraz', '/c'),
(813, 'Commande', 'Esteban_Alcaraz', '/ch'),
(814, 'Commande', 'Esteban_Alcaraz', '/r test'),
(815, 'Commande', 'Esteban_Alcaraz', '/b'),
(816, 'Commande', 'Esteban_Alcaraz', '/bc'),
(817, 'Commande', 'Esteban_Alcaraz', '/b'),
(818, 'Commande', 'Esteban_Alcaraz', '/bc'),
(819, 'Commande', 'Aaron_Iverson', '/B'),
(820, 'Commande', 'Aaron_Iverson', '/bc'),
(821, 'Commande', 'Esteban_Alcaraz', '/creerunité'),
(822, 'Commande', 'Esteban_Alcaraz', '/creerunite'),
(823, 'Commande', 'Aaron_Iverson', '/rejoindreunité &'),
(824, 'Commande', 'Aaron_Iverson', '/rejoindreunité 1'),
(825, 'Commande', 'Aaron_Iverson', '/creerunité '),
(826, 'Commande', 'Esteban_Alcaraz', '/rejoindreunite 2'),
(827, 'Commande', 'Aaron_Iverson', '/creerunite'),
(828, 'Commande', 'Aaron_Iverson', '/creerunite'),
(829, 'Commande', 'Aaron_Iverson', '/creerunite '),
(830, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(831, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(832, 'Commande', 'Esteban_Alcaraz', '/getincar 12'),
(833, 'Commande', 'Esteban_Alcaraz', '/creerunite'),
(834, 'Commande', 'Esteban_Alcaraz', '/getincar 13'),
(835, 'Commande', 'Esteban_Alcaraz', '/creerunite'),
(836, 'Commande', 'Aaron_Iverson', '/creerunite'),
(837, 'Commande', 'Esteban_Alcaraz', '/Quitterunite'),
(838, 'Message', 'Aaron_Iverson', 'Salut'),
(839, 'Commande', 'Aaron_Iverson', '/creerunite'),
(840, 'Commande', 'Esteban_Alcaraz', '/rejoindreunite 1'),
(841, 'Commande', 'Aaron_Iverson', '/dl true'),
(842, 'Commande', 'Aaron_Iverson', '/setessence 100 12'),
(843, 'Commande', 'Esteban_Alcaraz', '/setadminlevel'),
(844, 'Commande', 'Esteban_Alcaraz', '/setadminlevel aa 7'),
(845, 'Commande', 'Aaron_Iverson', '/setessence 100 12'),
(846, 'Commande', 'Aaron_Iverson', '/setessence 12 100'),
(847, 'Commande', 'Aaron_Iverson', '/dl false'),
(848, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(849, 'Commande', 'Aaron_Iverson', '/lspd'),
(850, 'Commande', 'Aaron_Iverson', '/getincar 12'),
(851, 'Commande', 'Aaron_Iverson', '/creerunite'),
(852, 'Commande', 'Esteban_Alcaraz', '/rejoindreunite 1'),
(853, 'Commande', 'Esteban_Alcaraz', '/rejoindreunite 1'),
(854, 'Commande', 'Esteban_Alcaraz', '/rejoindreunite 0'),
(855, 'Commande', 'Esteban_Alcaraz', '/quitterunite'),
(856, 'Commande', 'Esteban_Alcaraz', '/creerunite'),
(857, 'Commande', 'Esteban_Alcaraz', '/quitterunite'),
(858, 'Commande', 'Esteban_Alcaraz', '/creerunite'),
(859, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sanctus'),
(860, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sanctus'),
(861, 'Commande', 'Aaron_Iverson', '/spawnvehicule BMX'),
(862, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule bmx'),
(863, 'Commande', 'Esteban_Alcaraz', '/sethp esteban 100'),
(864, 'Commande', 'Esteban_Alcaraz', '/fc'),
(865, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(866, 'Commande', 'Aaron_Iverson', '/areanime Aa'),
(867, 'Commande', 'Aaron_Iverson', '/areanime'),
(868, 'Commande', 'Aaron_Iverson', '/b'),
(869, 'Commande', 'Aaron_Iverson', '/bc'),
(870, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(871, 'Commande', 'Esteban_Alcaraz', '/goto aa'),
(872, 'Message', 'Esteban_Alcaraz', ' /areanimer'),
(873, 'Commande', 'Esteban_Alcaraz', '/getcar 37'),
(874, 'Commande', 'Aaron_Iverson', '/areanimer Aa'),
(875, 'Commande', 'Aaron_Iverson', '/dl false'),
(876, 'Commande', 'Esteban_Alcaraz', '/areanimer est'),
(877, 'Commande', 'Esteban_Alcaraz', '/goto aa'),
(878, 'Commande', 'Aaron_Iverson', '/getvehicule zantorno'),
(879, 'Commande', 'Esteban_Alcaraz', '/areanimer est'),
(880, 'Commande', 'Aaron_Iverson', '/areanimer Aa'),
(881, 'Commande', 'Aaron_Iverson', '/spawnvehicule zentorno'),
(882, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule x20'),
(883, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule x21'),
(884, 'Commande', 'Esteban_Alcaraz', '/sethp esteb 100'),
(885, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(886, 'Commande', 'Esteban_Alcaraz', '/gotocar 28'),
(887, 'Commande', 'Esteban_Alcaraz', '/getincar 38'),
(888, 'Commande', 'Esteban_Alcaraz', '/getincar 34'),
(889, 'Commande', 'Esteban_Alcaraz', '/getincar 33'),
(890, 'Commande', 'Esteban_Alcaraz', '/getincar 32'),
(891, 'Commande', 'Esteban_Alcaraz', '/getincar 31'),
(892, 'Commande', 'Esteban_Alcaraz', '/getincar 30'),
(893, 'Commande', 'Esteban_Alcaraz', '/getincar 29'),
(894, 'Commande', 'Esteban_Alcaraz', '/getincar 29'),
(895, 'Commande', 'Esteban_Alcaraz', '/getincar 28'),
(896, 'Commande', 'Esteban_Alcaraz', '/getincar 27'),
(897, 'Commande', 'Esteban_Alcaraz', '/getincar 26'),
(898, 'Commande', 'Esteban_Alcaraz', '/getincar 25'),
(899, 'Commande', 'Esteban_Alcaraz', '/goto aa'),
(900, 'Commande', 'Esteban_Alcaraz', '/getcar 26'),
(901, 'Commande', 'Esteban_Alcaraz', '/getcar 25'),
(902, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(903, 'Commande', 'Esteban_Alcaraz', '/vr 38'),
(904, 'Commande', 'Esteban_Alcaraz', '/vr 38'),
(905, 'Commande', 'Aaron_Iverson', '/vr'),
(906, 'Commande', 'Esteban_Alcaraz', '/vr 38'),
(907, 'Commande', 'Esteban_Alcaraz', '/vr 38'),
(908, 'Commande', 'Esteban_Alcaraz', '/monaie'),
(909, 'Commande', 'Esteban_Alcaraz', '/monaies'),
(910, 'Commande', 'Esteban_Alcaraz', '/money'),
(911, 'Commande', 'Aaron_Iverson', '/money'),
(912, 'Commande', 'Esteban_Alcaraz', '/monnaie'),
(913, 'Commande', 'Aaron_Iverson', '/monnaie'),
(914, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule elegy'),
(915, 'Commande', 'Aaron_Iverson', '/giveweapon RPG'),
(916, 'Commande', 'Aaron_Iverson', '/giveweapon Aa RPG &À'),
(917, 'Commande', 'Aaron_Iverson', '/giveweapon Aa RPG 10'),
(918, 'Commande', 'Aaron_Iverson', '/DL TRUE'),
(919, 'Commande', 'Aaron_Iverson', '/dl true'),
(920, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(921, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(922, 'Commande', 'Esteban_Alcaraz', '/goto aa'),
(923, 'Commande', 'Esteban_Alcaraz', '/getcar 39'),
(924, 'Commande', 'Aaron_Iverson', '/dl false'),
(925, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(926, 'Commande', 'Esteban_Alcaraz', '/vr 39'),
(927, 'Commande', 'Esteban_Alcaraz', '/vr 39'),
(928, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(929, 'Commande', 'Esteban_Alcaraz', '/pos'),
(930, 'Commande', 'Esteban_Alcaraz', '/creergarage'),
(931, 'Commande', 'Esteban_Alcaraz', '/creergarage 2'),
(932, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(933, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(934, 'Commande', 'Aaron_Iverson', '/setessence 28 100'),
(935, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(936, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(937, 'Commande', 'Aaron_Iverson', '/areanimer AA'),
(938, 'Commande', 'Esteban_Alcaraz', '/areanimer est'),
(939, 'Commande', 'Aaron_Iverson', '/spawnvehicule pigalle'),
(940, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule btype2'),
(941, 'Commande', 'Aaron_Iverson', '/spawnvehicule vagner'),
(942, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule hauler2'),
(943, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule phantom3'),
(944, 'Commande', 'Aaron_Iverson', '/tp ESte'),
(945, 'Commande', 'Aaron_Iverson', '/tp aa estee'),
(946, 'Commande', 'Aaron_Iverson', '/tp aa este'),
(947, 'Commande', 'Aaron_Iverson', '/Areanimer a'),
(948, 'Commande', 'Aaron_Iverson', '/Areanimer aa'),
(949, 'Commande', 'Aaron_Iverson', '/tp aa este'),
(950, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule phantom2'),
(951, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule apc'),
(952, 'Commande', 'Esteban_Alcaraz', '/fc'),
(953, 'Commande', 'Aaron_Iverson', '/fc'),
(954, 'Commande', 'Esteban_Alcaraz', '/fcoff'),
(955, 'Commande', 'Esteban_Alcaraz', '/areanimer este'),
(956, 'Commande', 'Aaron_Iverson', '/fc'),
(957, 'Commande', 'Aaron_Iverson', '/fc off'),
(958, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule halftrack'),
(959, 'Commande', 'Aaron_Iverson', '/fcoff'),
(960, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule halftrack'),
(961, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule trailersmall2'),
(962, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule opressor'),
(963, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule 884483972'),
(964, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule 884483972'),
(965, 'Commande', 'Aaron_Iverson', '/areanimer'),
(966, 'Commande', 'Aaron_Iverson', '/areanimer aa'),
(967, 'Commande', 'Esteban_Alcaraz', '/areanimer est'),
(968, 'Commande', 'Esteban_Alcaraz', '/getcar 28'),
(969, 'Commande', 'Esteban_Alcaraz', '/getcarid'),
(970, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(971, 'Commande', 'Esteban_Alcaraz', '/getvehid'),
(972, 'Commande', 'Esteban_Alcaraz', '/getvehid'),
(973, 'Commande', 'Esteban_Alcaraz', '/getvehdbid 28'),
(974, 'Commande', 'Esteban_Alcaraz', '/gotopos'),
(975, 'Commande', 'Esteban_Alcaraz', '/gotopos -23.92922 -718.1271 32.59769'),
(976, 'Commande', 'Esteban_Alcaraz', '/gotocar 27'),
(977, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(978, 'Commande', 'Esteban_Alcaraz', '/gotocar 26'),
(979, 'Commande', 'Esteban_Alcaraz', '/getcar 28'),
(980, 'Commande', 'Esteban_Alcaraz', '/getvehdbid'),
(981, 'Commande', 'Esteban_Alcaraz', '/getvehdbid 28'),
(982, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(983, 'Commande', 'Esteban_Alcaraz', '/reloadconcess'),
(984, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(985, 'Commande', 'Esteban_Alcaraz', '/getvehdbid 31'),
(986, 'Commande', 'Esteban_Alcaraz', '/getvehdbid 28'),
(987, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(988, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(989, 'Commande', 'Esteban_Alcaraz', '/reloadconcess'),
(990, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(991, 'Commande', 'Esteban_Alcaraz', '/reloadconcess'),
(992, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(993, 'Commande', 'Esteban_Alcaraz', '/reloadconcess'),
(994, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(995, 'Commande', 'Esteban_Alcaraz', '/reloadconcess');
INSERT INTO `LogTextJoueur` (`ID`, `Type`, `Joueur`, `Message`) VALUES
(996, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(997, 'Commande', 'Esteban_Alcaraz', '/getvehdbid 30'),
(998, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(999, 'Commande', 'Esteban_Alcaraz', '/reloadconcess'),
(1000, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(1001, 'Commande', 'Esteban_Alcaraz', '/getvehdbid 30'),
(1002, 'Commande', 'Esteban_Alcaraz', '/getvehdbid 29'),
(1003, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(1004, 'Commande', 'Esteban_Alcaraz', '/rstartgf'),
(1005, 'Commande', 'Esteban_Alcaraz', '/restartgf'),
(1006, 'Commande', 'Esteban_Alcaraz', '/reloadconcess'),
(1007, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(1008, 'Commande', 'Esteban_Alcaraz', '/getvehdbid 29'),
(1009, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(1010, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule stretch'),
(1011, 'Commande', 'Esteban_Alcaraz', '/debug'),
(1012, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule stretch'),
(1013, 'Commande', 'Esteban_Alcaraz', '/startgf modmenu'),
(1014, 'Commande', 'Esteban_Alcaraz', '/getvehid'),
(1015, 'Commande', 'Esteban_Alcaraz', '/vr 34'),
(1016, 'Message', 'Mickael_Adams', 'Yo man;'),
(1017, 'Message', 'Mickael_Adams', ';'),
(1018, 'Message', 'Mickael_Adams', '.'),
(1019, 'Commande', 'Esteban_Alcaraz', '/vr 34'),
(1020, 'Commande', 'Esteban_Alcaraz', '/ainvite'),
(1021, 'Commande', 'Esteban_Alcaraz', '/ainvite micka 1'),
(1022, 'Commande', 'Esteban_Alcaraz', '/sethp esta 100'),
(1023, 'Commande', 'Esteban_Alcaraz', '/sethp este 100'),
(1024, 'Commande', 'Esteban_Alcaraz', '/areanimer mick'),
(1025, 'Message', 'Esteban_Alcaraz', ' /creerunité'),
(1026, 'Message', 'Esteban_Alcaraz', ' /creerunite'),
(1027, 'Message', 'Mickael_Adams', ':creerunite'),
(1028, 'Commande', 'Mickael_Adams', '/creerunite'),
(1029, 'Commande', 'Esteban_Alcaraz', '/rejoindreunite 1'),
(1030, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(1031, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(1032, 'Commande', 'Esteban_Alcaraz', '/r //.'),
(1033, 'Commande', 'Esteban_Alcaraz', '/vr 12'),
(1034, 'Commande', 'Esteban_Alcaraz', '/vr 12'),
(1035, 'Commande', 'Esteban_Alcaraz', '/lspd'),
(1036, 'Commande', 'Esteban_Alcaraz', '/gethere micka'),
(1037, 'Commande', 'Esteban_Alcaraz', '/gotocar 12'),
(1038, 'Commande', 'Esteban_Alcaraz', '/gethere micka'),
(1039, 'Commande', 'Esteban_Alcaraz', '/lspd'),
(1040, 'Commande', 'Esteban_Alcaraz', '/gethere micka'),
(1041, 'Commande', 'Esteban_Alcaraz', '/gotocar 12'),
(1042, 'Commande', 'Esteban_Alcaraz', '/gethere micka'),
(1043, 'Commande', 'Esteban_Alcaraz', '/ainvitejob micka 1'),
(1044, 'Commande', 'Esteban_Alcaraz', '/getvehid'),
(1045, 'Commande', 'Esteban_Alcaraz', '/setessence 1 100'),
(1046, 'Commande', 'Esteban_Alcaraz', '/vr 1'),
(1047, 'Commande', 'Esteban_Alcaraz', '/stopjob'),
(1048, 'Commande', 'Mickael_Adams', '/stopjob'),
(1049, 'Commande', 'Mickael_Adams', '/'),
(1050, 'Commande', 'Esteban_Alcaraz', '/getcar 25'),
(1051, 'Commande', 'Esteban_Alcaraz', '/vr 25'),
(1052, 'Commande', 'Mickael_Adams', '/me rit'),
(1053, 'Commande', 'Esteban_Alcaraz', '/sethp este 100'),
(1054, 'Commande', 'Esteban_Alcaraz', '/areanimer mic'),
(1055, 'Commande', 'Esteban_Alcaraz', '/sethp mic 100'),
(1056, 'Commande', 'Esteban_Alcaraz', '/getincar 25'),
(1057, 'Commande', 'Esteban_Alcaraz', '/goto mick'),
(1058, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule pigalle'),
(1059, 'Commande', 'Esteban_Alcaraz', '/payer mick 50000'),
(1060, 'Message', 'Esteban_Alcaraz', ' /trafiquerarme'),
(1061, 'Commande', 'Esteban_Alcaraz', '/trafiquerarme'),
(1062, 'Commande', 'Mickael_Adams', '/trafiquerarme'),
(1063, 'Commande', 'Esteban_Alcaraz', '/areanimer mick'),
(1064, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule elegy'),
(1065, 'Commande', 'Esteban_Alcaraz', '/getvehid'),
(1066, 'Commande', 'Esteban_Alcaraz', '/vr 3'),
(1067, 'Commande', 'Esteban_Alcaraz', '/vr 36'),
(1068, 'Commande', 'Esteban_Alcaraz', '/sethp este 100'),
(1069, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule speeder'),
(1070, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule shark'),
(1071, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule seashark'),
(1072, 'Commande', 'Esteban_Alcaraz', '/areanimer mick'),
(1073, 'Commande', 'Esteban_Alcaraz', '/gethere mick'),
(1074, 'Commande', 'Esteban_Alcaraz', '/areanimer mick'),
(1075, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule seashark'),
(1076, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule seashark2'),
(1077, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule seashark3'),
(1078, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule dinghy4'),
(1079, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule jetmax'),
(1080, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule tug'),
(1081, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule submersible'),
(1082, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule submersible2'),
(1083, 'Commande', 'Esteban_Alcaraz', '/lspd'),
(1084, 'Commande', 'Esteban_Alcaraz', '/areanimer este'),
(1085, 'Commande', 'Mickael_Adams', '/anim'),
(1086, 'Commande', 'Travis_Morgan', '/gotols'),
(1087, 'Commande', 'Travis_Morgan', '/pdp'),
(1088, 'Commande', 'Travis_Morgan', '/police'),
(1089, 'Commande', 'Travis_Morgan', '/police'),
(1090, 'Commande', 'Travis_Morgan', '/vehicule'),
(1091, 'Commande', 'Travis_Morgan', '/save'),
(1092, 'Commande', 'Travis_Morgan', '/save test'),
(1093, 'Commande', 'Mickael_Adams', '/goto'),
(1094, 'Commande', 'Mickael_Adams', '/goto Mickael'),
(1095, 'Commande', 'Mickael_Adams', '/stats'),
(1096, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(1097, 'Commande', 'Esteban_Alcaraz', '/vr 19'),
(1098, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(1099, 'Commande', 'Esteban_Alcaraz', '/vr 19'),
(1100, 'Commande', 'Esteban_Alcaraz', '/vr 19'),
(1101, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule elegy'),
(1102, 'Commande', 'Esteban_Alcaraz', '/startgf modmenu'),
(1103, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule serrano'),
(1104, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule xls'),
(1105, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule xls2'),
(1106, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule baller6'),
(1107, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule Cavalcade2'),
(1108, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule Cog55'),
(1109, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule Cog552'),
(1110, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule Cognoscenti'),
(1111, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule Cognoscenti2'),
(1112, 'Commande', 'Esteban_Alcaraz', '/dl true'),
(1113, 'Commande', 'Esteban_Alcaraz', '/dl false'),
(1114, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule BestiaGTS'),
(1115, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule CogCabrio'),
(1116, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sentinel'),
(1117, 'Commande', 'Esteban_Alcaraz', '/spawnvehicule sentinel2');

-- --------------------------------------------------------

--
-- Structure de la table `Logements`
--

CREATE TABLE IF NOT EXISTS `Logements` (
`ID` int(11) NOT NULL,
  `model` text NOT NULL,
  `PosX` float DEFAULT NULL,
  `PosY` float DEFAULT NULL,
  `PosZ` float DEFAULT NULL,
  `proprietaire` text NOT NULL,
  `Prix` int(11) NOT NULL,
  `Ouvert` tinyint(1) NOT NULL,
  `IDLocataire` int(11) NOT NULL,
  `IDColocataire` int(11) NOT NULL,
  `EnLocation` tinyint(4) NOT NULL,
  `PrixLocation` int(11) NOT NULL
) ENGINE=MyISAM AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `Logements`
--

INSERT INTO `Logements` (`ID`, `model`, `PosX`, `PosY`, `PosZ`, `proprietaire`, `Prix`, `Ouvert`, `IDLocataire`, `IDColocataire`, `EnLocation`, `PrixLocation`) VALUES
(1, 'app1', 114.074, -1960.82, 21.3342, 'Esteban_Alcaraz', 100, 1, -1, 2, 0, 0),
(2, 'app1', 85.5758, -1959.55, 21.1217, 'Aaron_Iverson', 100, 1, -1, -1, 0, 0),
(3, 'app1', 126.655, -1929.67, 21.3824, 'Aucun', 100, 1, -1, -1, 0, 0),
(4, 'app1', 118.009, -1921.16, 21.3234, 'Aucun', 100, 1, -1, -1, 0, 0),
(5, 'app1', 100.692, -1913.02, 21.2055, 'Aucun', 100, 1, -1, -1, 0, 0);

-- --------------------------------------------------------

--
-- Structure de la table `ModVehicules`
--

CREATE TABLE IF NOT EXISTS `ModVehicules` (
`ID` int(11) NOT NULL,
  `IDVeh` int(11) NOT NULL,
  `Spoilers` int(11) NOT NULL,
  `FrontBumper` int(11) NOT NULL,
  `RearBumper` int(11) NOT NULL,
  `SideSkirt` int(11) NOT NULL,
  `Exhaust` int(11) NOT NULL,
  `Frame` int(11) NOT NULL,
  `Grille` int(11) NOT NULL,
  `Hood` int(11) NOT NULL,
  `Fender` int(11) NOT NULL,
  `RightFender` int(11) NOT NULL,
  `Roof` int(11) NOT NULL,
  `Engine` int(11) NOT NULL,
  `Brakes` int(11) NOT NULL,
  `Transmission` int(11) NOT NULL,
  `Horns` int(11) NOT NULL,
  `Suspension` int(11) NOT NULL,
  `Armor` int(11) NOT NULL,
  `Turbo` int(11) NOT NULL,
  `Xenon` int(11) NOT NULL,
  `FrontWheels` int(11) NOT NULL,
  `BackWheels` int(11) NOT NULL,
  `PlateHolders` int(11) NOT NULL,
  `TrimDesign` int(11) NOT NULL,
  `Ornaments` int(11) NOT NULL,
  `DialDesign` int(11) NOT NULL,
  `SteeringWheel` int(11) NOT NULL,
  `ShiftLever` int(11) NOT NULL,
  `Plaques` int(11) NOT NULL,
  `Hydraulics` int(11) NOT NULL,
  `Livery` int(11) NOT NULL,
  `Plate` int(11) NOT NULL,
  `Color1` int(11) NOT NULL,
  `Color2` int(11) NOT NULL,
  `WindowTint` int(11) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `ModVehicules`
--

INSERT INTO `ModVehicules` (`ID`, `IDVeh`, `Spoilers`, `FrontBumper`, `RearBumper`, `SideSkirt`, `Exhaust`, `Frame`, `Grille`, `Hood`, `Fender`, `RightFender`, `Roof`, `Engine`, `Brakes`, `Transmission`, `Horns`, `Suspension`, `Armor`, `Turbo`, `Xenon`, `FrontWheels`, `BackWheels`, `PlateHolders`, `TrimDesign`, `Ornaments`, `DialDesign`, `SteeringWheel`, `ShiftLever`, `Plaques`, `Hydraulics`, `Livery`, `Plate`, `Color1`, `Color2`, `WindowTint`) VALUES
(1, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(2, 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(3, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(4, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(5, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(6, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(7, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(8, 9, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(9, 10, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(10, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(11, 12, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(12, 13, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(13, 14, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(14, 15, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(15, 16, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(16, 17, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(17, 18, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(18, 19, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(19, 20, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(20, 21, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(21, 24, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(22, 22, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(23, 102, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(24, 101, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(25, 100, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(26, 98, 8, 5, 5, 6, 13, 2, 8, 11, 9, -1, 0, 4, 4, 4, -1, 3, 4, 4, -1, 18, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 1, 8, 0, 1),
(27, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(28, 92, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 4, 4, 4, -1, 3, 4, 4, -1, 18, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 1, 0, 0, 1),
(29, 103, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(30, 109, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(31, 108, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(32, 107, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(33, 106, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(34, 104, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(35, 105, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1),
(36, 110, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1);

-- --------------------------------------------------------

--
-- Structure de la table `PlanqueFaction`
--

CREATE TABLE IF NOT EXISTS `PlanqueFaction` (
`ID` int(11) NOT NULL,
  `nominte` text NOT NULL,
  `factionid` int(11) DEFAULT NULL,
  `PosX` float DEFAULT NULL,
  `PosY` float DEFAULT NULL,
  `PosZ` float DEFAULT NULL,
  `proprietaire` text NOT NULL,
  `locked` tinyint(1) DEFAULT NULL,
  `kitpisto` int(11) DEFAULT NULL,
  `kitpmitr` int(11) DEFAULT NULL,
  `kitpompe` int(11) DEFAULT NULL,
  `kitfusil` int(11) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `PlanqueFaction`
--

INSERT INTO `PlanqueFaction` (`ID`, `nominte`, `factionid`, `PosX`, `PosY`, `PosZ`, `proprietaire`, `locked`, `kitpisto`, `kitpmitr`, `kitpompe`, `kitfusil`) VALUES
(1, 'InteArmes', 3, -116.162, -1772.37, 29.8593, 'Ange_Graziani', 0, 0, 0, 0, 0);

-- --------------------------------------------------------

--
-- Structure de la table `PompesEssences`
--

CREATE TABLE IF NOT EXISTS `PompesEssences` (
`ID` int(11) NOT NULL,
  `IDBDDStation` int(11) NOT NULL,
  `PosX` float(8,4) NOT NULL,
  `PosY` float(8,4) NOT NULL,
  `PosZ` float NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `PompesEssences`
--

INSERT INTO `PompesEssences` (`ID`, `IDBDDStation`, `PosX`, `PosY`, `PosZ`) VALUES
(1, 1, -76.6058, -1755.7750, 29.8004),
(2, 1, -68.4298, -1758.6780, 29.5337),
(3, 2, 182.2660, -1562.8430, 29.2724);

-- --------------------------------------------------------

--
-- Structure de la table `PoubellesEboueurs`
--

CREATE TABLE IF NOT EXISTS `PoubellesEboueurs` (
`ID` int(11) NOT NULL,
  `PosX` float DEFAULT NULL,
  `PosY` float DEFAULT NULL,
  `PosZ` float DEFAULT NULL,
  `NbPoubelles` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `PucesTelephones`
--

CREATE TABLE IF NOT EXISTS `PucesTelephones` (
`ID` int(11) NOT NULL,
  `Numero` int(11) DEFAULT NULL,
  `IDJoueur` int(11) DEFAULT NULL,
  `Active` tinyint(4) DEFAULT NULL,
  `TempsAppelRestant` int(11) NOT NULL,
  `NombreSmsRestant` int(11) NOT NULL
) ENGINE=MyISAM AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `PucesTelephones`
--

INSERT INTO `PucesTelephones` (`ID`, `Numero`, `IDJoueur`, `Active`, `TempsAppelRestant`, `NombreSmsRestant`) VALUES
(1, 5733, 2, 0, 0, 0),
(2, 5277, 2, 0, 0, 0),
(3, 7071, 2, 1, 0, 0),
(8, 2334, 10, 1, 10, 2),
(6, 5450, 10, 0, 0, 0);

-- --------------------------------------------------------

--
-- Structure de la table `QuestionAutoEcole`
--

CREATE TABLE IF NOT EXISTS `QuestionAutoEcole` (
`ID` int(11) NOT NULL,
  `Question` longtext NOT NULL,
  `BR` longtext NOT NULL,
  `R2` longtext NOT NULL,
  `R3` longtext NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `QuestionAutoEcole`
--

INSERT INTO `QuestionAutoEcole` (`ID`, `Question`, `BR`, `R2`, `R3`) VALUES
(1, 'Comment tu avances?', 'En accelerant', 'En freinant', 'En enculant des meres'),
(2, 'Tu fais quoi a un feu rouge?', 'Tu t''arretes', 'Tu avance', 'je sais pas'),
(3, 'Tu fais quoi a un Stop?', 'Tu t''arretes', 'Tu continue sans t''arreter', 'Tu éteins le moteur');

-- --------------------------------------------------------

--
-- Structure de la table `StationsEssences`
--

CREATE TABLE IF NOT EXISTS `StationsEssences` (
`ID` int(11) NOT NULL,
  `PosX` float(8,4) NOT NULL,
  `PosY` float(8,4) NOT NULL,
  `PosZ` float(8,4) NOT NULL,
  `Stockage` int(11) NOT NULL,
  `Proprio` int(11) NOT NULL,
  `Argents` int(11) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `StationsEssences`
--

INSERT INTO `StationsEssences` (`ID`, `PosX`, `PosY`, `PosZ`, `Stockage`, `Proprio`, `Argents`) VALUES
(1, -70.5339, -1747.4980, 29.5146, 10000, -1, 0),
(2, 185.8588, -1574.2640, 29.2916, 10000, -1, 0);

-- --------------------------------------------------------

--
-- Structure de la table `Telephones`
--

CREATE TABLE IF NOT EXISTS `Telephones` (
`ID` int(11) NOT NULL,
  `IDJoueur` int(11) DEFAULT NULL,
  `Puce` int(11) DEFAULT NULL
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `Telephones`
--

INSERT INTO `Telephones` (`ID`, `IDJoueur`, `Puce`) VALUES
(1, 10, 8),
(2, 2, 3);

-- --------------------------------------------------------

--
-- Structure de la table `Utilisateur`
--

CREATE TABLE IF NOT EXISTS `Utilisateur` (
`ID` int(11) NOT NULL,
  `SCN` text,
  `PlayerName` text,
  `MDP` text,
  `lvl` int(11) DEFAULT '0',
  `adminlvl` tinyint(4) DEFAULT '0',
  `sexe` text,
  `bank` int(10) DEFAULT '4500',
  `money` int(10) DEFAULT '500',
  `PermisDeConduire` int(11) NOT NULL DEFAULT '0',
  `cuntdownpaye` int(11) NOT NULL DEFAULT '60',
  `pendingpaye` int(11) NOT NULL DEFAULT '0',
  `pendingamende` int(11) NOT NULL DEFAULT '0',
  `sante` int(11) NOT NULL DEFAULT '100',
  `armure` int(11) NOT NULL DEFAULT '0',
  `jobid` int(11) DEFAULT '0',
  `factionid` int(11) DEFAULT '0',
  `rangfaction` int(11) NOT NULL DEFAULT '0',
  `DescriptionFaction` text NOT NULL,
  `entrepriseid` int(11) NOT NULL DEFAULT '0',
  `rangentreprise` int(11) NOT NULL DEFAULT '0',
  `IsOnPlanqueArmes` tinyint(1) NOT NULL DEFAULT '0',
  `IsOnPlanqueDrogues` tinyint(1) NOT NULL DEFAULT '0',
  `TimerKitArmes` int(11) NOT NULL DEFAULT '0',
  `PosX` float(8,4) DEFAULT NULL,
  `PosY` float(8,4) DEFAULT NULL,
  `PosZ` float(8,4) DEFAULT NULL,
  `RotX` float(8,4) DEFAULT NULL,
  `RotY` float(8,4) DEFAULT NULL,
  `RotZ` float(8,4) DEFAULT NULL,
  `IsOnInt` tinyint(1) NOT NULL DEFAULT '0',
  `dimension` int(11) NOT NULL DEFAULT '0',
  `IsMenotter` tinyint(1) NOT NULL DEFAULT '0',
  `IsDead` tinyint(1) DEFAULT '0',
  `IsBan` tinyint(1) NOT NULL DEFAULT '0',
  `registred` timestamp NULL DEFAULT NULL
) ENGINE=MyISAM AUTO_INCREMENT=23 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `Utilisateur`
--

INSERT INTO `Utilisateur` (`ID`, `SCN`, `PlayerName`, `MDP`, `lvl`, `adminlvl`, `sexe`, `bank`, `money`, `PermisDeConduire`, `cuntdownpaye`, `pendingpaye`, `pendingamende`, `sante`, `armure`, `jobid`, `factionid`, `rangfaction`, `DescriptionFaction`, `entrepriseid`, `rangentreprise`, `IsOnPlanqueArmes`, `IsOnPlanqueDrogues`, `TimerKitArmes`, `PosX`, `PosY`, `PosZ`, `RotX`, `RotY`, `RotZ`, `IsOnInt`, `dimension`, `IsMenotter`, `IsDead`, `IsBan`, `registred`) VALUES
(2, 'portos060', 'Ange_Graziani', '26429a356b1d25b7d57c0f9a6d5fed8a290cb42374185887dcd2874548df0779', 0, 7, 'Homme', 68300, 129482, -1, 24, 0, 0, 100, 0, 0, 1, 1, '', 0, 0, 1, 0, 0, 342.8220, -2640.4431, 6.2216, 0.0000, 0.0000, -95.4425, 1, 0, 0, 0, 0, '2017-03-23 11:56:43'),
(10, 'MrJaimeLaPatat', 'Melinda_Cortex', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', 0, 7, 'Homme', 81250, 974755, -1, 39, 0, 0, 90, 0, 1, 1, 1, '', 0, 0, 0, 0, 0, 576.0143, -1976.3430, 17.5318, 0.0114, -0.0159, 127.9511, 0, 0, 0, 0, 0, '2017-03-23 23:55:20'),
(18, 'Polidoor', 'Travis_Morgan', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', 0, 7, 'Homme', 4500, 500, -1, 23, 0, 0, 100, 0, 0, 0, 0, '', 0, 0, 0, 0, 0, -504.7199, -865.6581, 29.8725, 0.0000, 0.0000, -117.2168, 0, 0, 0, 0, 0, '2017-05-31 01:03:47'),
(19, 'MrJaimeLaPatate', 'Esteban_Alcaraz', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', 0, 7, 'Homme', 1008100, 938582, -1, 9, 0, 0, 40, 0, 1, 1, 1, '', 0, 0, 0, 0, 0, -30.6300, -2131.3860, 9.7117, -0.1421, -1.2784, -66.2494, 0, 0, 0, 0, 0, '2017-07-14 02:18:47'),
(20, 'Layzrod', 'Wendell_Rose', 'f1399af7a3e63cc81f67b3452710440971f364102c24b78843078a87282918dc', 0, 0, 'Homme', 4500, 500, -1, 60, 0, 0, 75, 0, 0, 0, 0, '', 0, 0, 0, 0, 0, 417.8196, -2003.0240, 22.7351, 0.4861, 1.7310, -133.3351, 0, 0, 0, 0, 0, '2017-07-24 02:31:11'),
(21, 'MPSUPER', 'Aaron_Iverson', '19936485ba8bbd28bde0f8491cd7fc16aa71e1b30cc153ed0bec44829e46eabf', 0, 7, 'Homme', 900, 34350, -1, 14, 0, 0, 10, 0, 1, 1, 1, '', 0, 0, 0, 0, 0, -1346.6140, -306.6953, 39.2678, 0.0000, 0.0000, 153.5424, 0, 0, 0, 1, 0, '2017-08-04 16:56:57'),
(22, 'PepitoOff', 'Mickael_Adams', 'c2248fce58070c9f97a6fcd13724a56df244ffb6bce7b2b74048c986dd300d61', 0, 0, 'Homme', 4950, 46450, -1, 57, 0, 0, 26, 0, 1, 1, 1, '', 0, 0, 0, 0, 0, -1027.8051, -1304.8330, 6.1234, 0.0000, 0.0000, 60.0187, 0, 0, 0, 0, 0, '2017-08-06 23:27:00');

-- --------------------------------------------------------

--
-- Structure de la table `UtilisateurVetements`
--

CREATE TABLE IF NOT EXISTS `UtilisateurVetements` (
`ID` int(11) NOT NULL,
  `PlayerName` text,
  `draw0` int(11) DEFAULT NULL,
  `draw1` int(11) DEFAULT NULL,
  `draw2` int(11) DEFAULT NULL,
  `draw3` int(11) DEFAULT NULL,
  `draw4` int(11) DEFAULT NULL,
  `draw5` int(11) DEFAULT NULL,
  `draw6` int(11) DEFAULT NULL,
  `draw7` int(11) DEFAULT NULL,
  `draw8` int(11) DEFAULT NULL,
  `draw9` int(11) DEFAULT NULL,
  `draw10` int(11) DEFAULT NULL,
  `draw11` int(11) DEFAULT NULL,
  `tx0` int(11) DEFAULT NULL,
  `tx1` int(11) DEFAULT NULL,
  `tx2` int(11) DEFAULT NULL,
  `tx3` int(11) DEFAULT NULL,
  `tx4` int(11) DEFAULT NULL,
  `tx5` int(11) DEFAULT NULL,
  `tx6` int(11) DEFAULT NULL,
  `tx7` int(11) DEFAULT NULL,
  `tx8` int(11) DEFAULT NULL,
  `tx9` int(11) DEFAULT NULL,
  `tx10` int(11) DEFAULT NULL,
  `tx11` int(11) DEFAULT NULL,
  `propdraw0` int(11) DEFAULT NULL,
  `propdraw1` int(11) DEFAULT NULL,
  `propdraw2` int(11) DEFAULT NULL,
  `propdraw3` int(11) DEFAULT NULL,
  `propdraw4` int(11) DEFAULT NULL,
  `propdraw5` int(11) DEFAULT NULL,
  `propdraw6` int(11) DEFAULT NULL,
  `propdraw7` int(11) DEFAULT NULL,
  `propdraw8` int(11) DEFAULT NULL,
  `propdraw9` int(11) DEFAULT NULL,
  `proptx0` int(11) DEFAULT NULL,
  `proptx1` int(11) DEFAULT NULL,
  `proptx2` int(11) DEFAULT NULL,
  `proptx3` int(11) DEFAULT NULL,
  `proptx4` int(11) DEFAULT NULL,
  `proptx5` int(11) DEFAULT NULL,
  `proptx6` int(11) DEFAULT NULL,
  `proptx7` int(11) DEFAULT NULL,
  `proptx8` int(11) DEFAULT NULL,
  `proptx9` int(11) DEFAULT NULL,
  `GTAO_HAS_CHARACTER_DATA` text,
  `GTAO_SHAPE_FIRST_ID` int(11) DEFAULT NULL,
  `GTAO_SHAPE_SECOND_ID` int(11) DEFAULT NULL,
  `GTAO_SKIN_FIRST_ID` int(11) DEFAULT NULL,
  `GTAO_SKIN_SECOND_ID` int(11) DEFAULT NULL,
  `GTAO_SHAPE_MIX` float DEFAULT NULL,
  `GTAO_SKIN_MIX` float DEFAULT NULL,
  `GTAO_HAIR_COLOR` int(11) DEFAULT NULL,
  `GTAO_HAIR_HIGHLIGHT_COLOR` int(11) DEFAULT NULL,
  `GTAO_EYE_COLOR` int(11) DEFAULT NULL,
  `GTAO_EYEBROWS` int(11) DEFAULT NULL,
  `GTAO_EYEBROWS_COLOR` int(11) DEFAULT NULL,
  `GTAO_MAKEUP_COLOR` int(11) DEFAULT NULL,
  `GTAO_LIPSTICK_COLOR` int(11) DEFAULT NULL,
  `GTAO_EYEBROWS_COLOR2` int(11) DEFAULT NULL,
  `GTAO_MAKEUP_COLOR2` int(11) DEFAULT NULL,
  `GTAO_LIPSTICK_COLOR2` int(11) DEFAULT NULL,
  `GTAO_MAKEUP` int(11) DEFAULT NULL,
  `GTAO_BEARD_COLOR` int(11) DEFAULT NULL,
  `GTAO_BEARD` int(11) DEFAULT NULL
) ENGINE=MyISAM AUTO_INCREMENT=34 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `UtilisateurVetements`
--

INSERT INTO `UtilisateurVetements` (`ID`, `PlayerName`, `draw0`, `draw1`, `draw2`, `draw3`, `draw4`, `draw5`, `draw6`, `draw7`, `draw8`, `draw9`, `draw10`, `draw11`, `tx0`, `tx1`, `tx2`, `tx3`, `tx4`, `tx5`, `tx6`, `tx7`, `tx8`, `tx9`, `tx10`, `tx11`, `propdraw0`, `propdraw1`, `propdraw2`, `propdraw3`, `propdraw4`, `propdraw5`, `propdraw6`, `propdraw7`, `propdraw8`, `propdraw9`, `proptx0`, `proptx1`, `proptx2`, `proptx3`, `proptx4`, `proptx5`, `proptx6`, `proptx7`, `proptx8`, `proptx9`, `GTAO_HAS_CHARACTER_DATA`, `GTAO_SHAPE_FIRST_ID`, `GTAO_SHAPE_SECOND_ID`, `GTAO_SKIN_FIRST_ID`, `GTAO_SKIN_SECOND_ID`, `GTAO_SHAPE_MIX`, `GTAO_SKIN_MIX`, `GTAO_HAIR_COLOR`, `GTAO_HAIR_HIGHLIGHT_COLOR`, `GTAO_EYE_COLOR`, `GTAO_EYEBROWS`, `GTAO_EYEBROWS_COLOR`, `GTAO_MAKEUP_COLOR`, `GTAO_LIPSTICK_COLOR`, `GTAO_EYEBROWS_COLOR2`, `GTAO_MAKEUP_COLOR2`, `GTAO_LIPSTICK_COLOR2`, `GTAO_MAKEUP`, `GTAO_BEARD_COLOR`, `GTAO_BEARD`) VALUES
(15, 'Ange_Graziani', 0, 0, 31, 6, 55, 0, 1, 0, 15, 0, 0, 113, 0, 0, 0, 0, 3, 0, 1, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '1', 1, 10, 1, 10, 0.35, 0.35, 0, 0, 0, 18, 0, 0, 0, 0, 0, 0, 0, 0, 2),
(28, 'Travis_Morgan', 0, 0, 0, 4, 9, 0, 38, 0, 15, 0, 0, 139, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '1', 0, 0, 0, 0, 0.35, 0.35, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
(30, 'Esteban_Alcaraz', 0, 0, 0, 0, 7, 0, 7, 89, 15, 0, 0, 80, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '1', 17, 0, 17, 0, 0.35, 0.35, 0, 0, 1, 17, 0, 0, 0, 0, 0, 0, 0, 0, 3),
(29, 'Melinda_Cortex', 0, 0, 31, 6, 55, 0, 1, 0, 15, 0, 0, 113, 0, 0, 0, 0, 3, 0, 1, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '1', 1, 10, 1, 10, 0.35, 0.35, 0, 0, 0, 18, 0, 0, 0, 0, 0, 0, 0, 0, 2),
(31, 'Wendell_Rose', 0, 0, 14, 4, 82, 0, 57, 89, 76, 0, 0, 191, 0, 0, 0, 0, 9, 0, 8, 1, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '1', 15, 0, 15, 0, 0.35, 0.35, 8, 8, 8, 13, 0, 0, 0, 0, 0, 0, 0, 17, 11),
(32, 'Aaron_Iverson', 0, 0, 0, 12, 24, 0, 18, 29, 31, 0, 0, 29, 0, 0, 0, 0, 5, 0, 0, 2, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '1', 4, 0, 4, 0, 0.35, 0.35, 0, 0, 19, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
(33, 'Mickael_Adams', 0, 0, 8, 35, 82, 0, 43, 0, 15, 0, 0, 187, 0, 0, 0, 0, 8, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '1', 3, 2, 3, 2, 0.35, 0.35, 55, 55, 2, 12, 0, 0, 0, 0, 0, 0, 0, 18, 7);

-- --------------------------------------------------------

--
-- Structure de la table `Vehicules`
--

CREATE TABLE IF NOT EXISTS `Vehicules` (
`ID` int(11) NOT NULL,
  `Proprio` int(11) DEFAULT '-1',
  `factionid` int(11) NOT NULL,
  `jobid` int(11) NOT NULL,
  `model` int(11) NOT NULL,
  `color1` int(11) NOT NULL,
  `color2` int(11) NOT NULL,
  `PosX` float(8,4) DEFAULT NULL,
  `PosY` float(8,4) DEFAULT NULL,
  `PosZ` float(8,4) DEFAULT NULL,
  `RotX` float(8,4) DEFAULT NULL,
  `RotY` float(8,4) DEFAULT NULL,
  `RotZ` float(8,4) DEFAULT NULL,
  `EnVente` int(11) NOT NULL,
  `PrixVente` int(11) NOT NULL,
  `Essence` int(11) NOT NULL,
  `Plaque` text NOT NULL,
  `Spawned` int(11) NOT NULL DEFAULT '1',
  `Locked` tinyint(1) NOT NULL DEFAULT '0',
  `Dimension` int(11) NOT NULL,
  `DansGarage` tinyint(1) NOT NULL
) ENGINE=MyISAM AUTO_INCREMENT=133 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `Vehicules`
--

INSERT INTO `Vehicules` (`ID`, `Proprio`, `factionid`, `jobid`, `model`, `color1`, `color2`, `PosX`, `PosY`, `PosZ`, `RotX`, `RotY`, `RotZ`, `EnVente`, `PrixVente`, `Essence`, `Plaque`, `Spawned`, `Locked`, `Dimension`, `DansGarage`) VALUES
(2, 0, 0, 1, 1747439474, 0, 0, 482.0981, -1390.0439, 28.9267, -0.5511, 0.3278, -88.0754, 0, 0, 0, '', 1, 0, 0, 0),
(3, 0, 0, 1, 1747439474, 0, 0, 483.0523, -1397.9950, 28.9091, -0.2965, 0.0095, -88.9930, 0, 0, 0, '', 1, 0, 0, 0),
(4, 0, 0, 1, 1747439474, 0, 0, 483.2411, -1403.9180, 28.8925, 0.2284, 0.1357, -90.1909, 0, 0, 71, '', 1, 0, 0, 0),
(5, 0, 0, 1, 1747439474, 0, 0, 497.0070, -1399.0780, 28.9866, 0.0588, -0.4416, 88.9854, 0, 0, 0, '', 1, 0, 0, 0),
(6, 0, 0, 1, 1747439474, 0, 0, 496.9685, -1404.1500, 28.9375, -0.2700, -0.9480, 90.0828, 0, 0, 0, '', 1, 0, 0, 0),
(7, 0, 0, 1, 1747439474, 0, 0, 497.0292, -1408.0150, 28.9033, 0.1870, -0.1313, 90.9999, 0, 0, 0, '', 1, 0, 0, 0),
(8, 0, 1, 0, -1627000575, 111, 0, 446.0091, -1025.8940, 28.2581, -0.2939, 1.0561, 5.9792, 0, 0, 98, '', 1, 0, 0, 0),
(9, 0, 1, 0, -1627000575, 111, 0, 442.5092, -1026.4041, 28.3268, -0.1802, 1.0739, 5.6207, 0, 0, 64, '', 1, 0, 0, 0),
(10, 0, 1, 0, 1912215274, 111, 0, 449.4878, -1025.6030, 28.3397, -0.1636, 1.0819, 7.5827, 0, 0, 92, '', 1, 0, 0, 0),
(11, 0, 1, 0, 1912215274, 111, 0, 452.8577, -1025.2330, 28.2748, -0.2582, 1.2897, 8.6660, 0, 0, 64, '', 1, 0, 0, 0),
(12, 0, 1, 0, -1973172295, 3, 0, 451.5760, -996.8126, 25.6136, -178.2090, -3.8307, -20.2431, 0, 0, 0, '', 1, 0, 0, 0),
(13, 0, 1, 0, -1973172295, 3, 0, 447.3010, -997.1696, 25.3662, -0.2563, -0.8012, 108.4013, 0, 0, 0, '', 1, 0, 0, 0),
(14, 0, 1, 0, 2046537925, 111, 0, 423.7881, -1028.7330, 28.6572, 0.0270, 0.9702, 3.5842, 0, 0, 95, '', 1, 0, 0, 0),
(15, 0, 1, 0, 2046537925, 111, 0, 431.3362, -1027.8990, 28.5250, -0.0386, 1.0180, 4.8795, 0, 0, 54, '', 1, 0, 0, 0),
(16, 0, 1, 0, 2046537925, 111, 0, 438.7209, -1027.0530, 28.3924, -0.1436, 1.0320, 4.6685, 0, 0, 99, '', 1, 0, 0, 0),
(17, 0, 1, 0, 2046537925, 111, 0, 435.0120, -1027.5850, 28.4593, -0.1150, 1.0290, 3.5416, 0, 0, 92, '', 1, 0, 0, 0),
(18, 0, 1, 0, -34623805, 0, 0, 464.9047, -1017.9890, 27.5628, 0.3354, -9.9950, 67.3723, 0, 0, 93, '', 1, 0, 0, 0),
(19, 0, 1, 0, -34623805, 0, 0, 465.1588, -1014.8010, 27.5509, -0.0497, -9.9991, 70.7399, 0, 0, 98, '', 1, 0, 0, 0),
(20, 0, 1, 0, -34623805, 0, 0, 464.9634, -1016.2250, 27.5533, -0.0498, -9.9973, 71.9992, 0, 0, 0, '', 1, 0, 0, 0),
(21, 0, 1, 0, 2046537925, 111, 0, 427.5296, -1028.3000, 28.5923, 0.0094, 1.0035, 4.7141, 0, 0, 74, '', 1, 0, 0, 0),
(24, 0, 1, 0, 353883353, 0, 0, 449.3674, -979.3466, 44.0802, 0.4256, -0.0076, 10.1755, 0, 0, 96, '', 1, 0, 0, 0),
(22, 0, 1, 0, -2007026063, 75, 0, 476.8514, -1024.0640, 28.3306, -1.1493, -0.5934, -82.9614, 0, 0, 88, '', 1, 0, 0, 0),
(102, 0, 2, 0, 1171614426, 0, 0, 327.8445, -1474.4230, 29.5477, -0.2696, -0.9746, -129.3430, 0, 0, 0, '', 1, 0, 0, 0),
(101, 0, 2, 0, 1171614426, 0, 0, 333.0508, -1476.6960, 29.4423, -2.5021, 1.0056, -60.9305, 0, 0, 0, '', 1, 0, 0, 0),
(100, 0, 2, 0, 1171614426, 0, 0, 339.5225, -1465.0649, 29.2985, -0.8667, 1.6769, -66.8992, 0, 0, 97, '', 1, 0, 0, 0),
(98, 19, 0, 0, 917809321, 3, 103, -0.0083, 0.0319, -155.9910, 0.9448, 0.7176, 0.0780, 0, 3599, 91, '111111', 1, 0, 0, 1),
(132, 0, 0, 0, 1078682497, 18, 29, -43.7763, -1689.3370, 28.6986, 0.6384, -0.1377, 48.8181, 1, 8799, 100, 'A VENDRE', 1, 0, 0, 0),
(130, 0, 0, 0, 1762279763, 135, 139, -51.0770, -1693.6300, 28.8177, 0.0230, -0.0243, 48.1566, 1, 3000, 100, 'A VENDRE', 1, 0, 0, 0),
(131, 0, 0, 0, 523724515, 100, 140, -47.1424, -1691.6429, 28.7559, 0.1671, 1.2382, 47.8727, 1, 3599, 100, 'A VENDRE', 1, 0, 0, 0),
(92, 19, 0, 0, 196747873, 82, 127, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0, 4000, 56, '', 1, 1, 1, 1),
(129, 0, 0, 0, 1078682497, 11, 138, -51.7538, -1677.4139, 28.6847, 0.5774, -1.5320, -128.7879, 1, 8799, 100, 'A VENDRE', 1, 0, 0, 0),
(128, 0, 0, 0, 523724515, 121, 57, -54.3373, -1680.4370, 28.7885, 0.2951, -1.4275, -129.1725, 1, 3599, 100, 'A VENDRE', 1, 0, 0, 0),
(127, 0, 0, 0, -1883002148, 110, 105, -56.6942, -1683.5940, 28.8174, 0.0086, 0.0008, -130.1186, 1, 4000, 100, 'A VENDRE', 1, 0, 0, 0),
(126, 0, 0, 0, 1762279763, 154, 87, -59.2328, -1686.3560, 28.8175, 0.0282, -0.0003, -130.0254, 1, 3000, 100, 'A VENDRE', 1, 0, 0, 0);

--
-- Index pour les tables exportées
--

--
-- Index pour la table `ATM`
--
ALTER TABLE `ATM`
 ADD PRIMARY KEY (`idATM`);

--
-- Index pour la table `CheckpointAutoEcole`
--
ALTER TABLE `CheckpointAutoEcole`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `ClefsLogements`
--
ALTER TABLE `ClefsLogements`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `ClefsVehicules`
--
ALTER TABLE `ClefsVehicules`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `Coffre`
--
ALTER TABLE `Coffre`
 ADD PRIMARY KEY (`id`);

--
-- Index pour la table `Concess`
--
ALTER TABLE `Concess`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `Entreprises`
--
ALTER TABLE `Entreprises`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `Garages`
--
ALTER TABLE `Garages`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `Inventaire`
--
ALTER TABLE `Inventaire`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `Item`
--
ALTER TABLE `Item`
 ADD PRIMARY KEY (`id`);

--
-- Index pour la table `LogTextJoueur`
--
ALTER TABLE `LogTextJoueur`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `Logements`
--
ALTER TABLE `Logements`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `ModVehicules`
--
ALTER TABLE `ModVehicules`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `PlanqueFaction`
--
ALTER TABLE `PlanqueFaction`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `PompesEssences`
--
ALTER TABLE `PompesEssences`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `PoubellesEboueurs`
--
ALTER TABLE `PoubellesEboueurs`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `PucesTelephones`
--
ALTER TABLE `PucesTelephones`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `QuestionAutoEcole`
--
ALTER TABLE `QuestionAutoEcole`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `StationsEssences`
--
ALTER TABLE `StationsEssences`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `Telephones`
--
ALTER TABLE `Telephones`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `Utilisateur`
--
ALTER TABLE `Utilisateur`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `UtilisateurVetements`
--
ALTER TABLE `UtilisateurVetements`
 ADD PRIMARY KEY (`ID`);

--
-- Index pour la table `Vehicules`
--
ALTER TABLE `Vehicules`
 ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT pour les tables exportées
--

--
-- AUTO_INCREMENT pour la table `ATM`
--
ALTER TABLE `ATM`
MODIFY `idATM` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=16;
--
-- AUTO_INCREMENT pour la table `CheckpointAutoEcole`
--
ALTER TABLE `CheckpointAutoEcole`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT pour la table `ClefsLogements`
--
ALTER TABLE `ClefsLogements`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT pour la table `ClefsVehicules`
--
ALTER TABLE `ClefsVehicules`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=8;
--
-- AUTO_INCREMENT pour la table `Concess`
--
ALTER TABLE `Concess`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=8;
--
-- AUTO_INCREMENT pour la table `Entreprises`
--
ALTER TABLE `Entreprises`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT pour la table `Garages`
--
ALTER TABLE `Garages`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT pour la table `Inventaire`
--
ALTER TABLE `Inventaire`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=42;
--
-- AUTO_INCREMENT pour la table `Item`
--
ALTER TABLE `Item`
MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=9;
--
-- AUTO_INCREMENT pour la table `LogTextJoueur`
--
ALTER TABLE `LogTextJoueur`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=1118;
--
-- AUTO_INCREMENT pour la table `Logements`
--
ALTER TABLE `Logements`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT pour la table `ModVehicules`
--
ALTER TABLE `ModVehicules`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=37;
--
-- AUTO_INCREMENT pour la table `PlanqueFaction`
--
ALTER TABLE `PlanqueFaction`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT pour la table `PompesEssences`
--
ALTER TABLE `PompesEssences`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT pour la table `PoubellesEboueurs`
--
ALTER TABLE `PoubellesEboueurs`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT pour la table `PucesTelephones`
--
ALTER TABLE `PucesTelephones`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=9;
--
-- AUTO_INCREMENT pour la table `QuestionAutoEcole`
--
ALTER TABLE `QuestionAutoEcole`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT pour la table `StationsEssences`
--
ALTER TABLE `StationsEssences`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT pour la table `Telephones`
--
ALTER TABLE `Telephones`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT pour la table `Utilisateur`
--
ALTER TABLE `Utilisateur`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=23;
--
-- AUTO_INCREMENT pour la table `UtilisateurVetements`
--
ALTER TABLE `UtilisateurVetements`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=34;
--
-- AUTO_INCREMENT pour la table `Vehicules`
--
ALTER TABLE `Vehicules`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=133;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
