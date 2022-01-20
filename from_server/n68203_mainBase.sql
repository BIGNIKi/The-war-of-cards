-- MySQL dump 10.13  Distrib 5.7.30, for Linux (x86_64)
--
-- Host: localhost    Database: n68203_mainBase
-- ------------------------------------------------------
-- Server version	5.7.30-cll-lve

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Current Database: `n68203_mainBase`
--


--
-- Table structure for table `general`
--

DROP TABLE IF EXISTS `general`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `general` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `time_when` varchar(191) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `login` varchar(191) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `text_msg` varchar(191) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `year_month_day` varchar(191) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=179 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `general`
--

LOCK TABLES `general` WRITE;
/*!40000 ALTER TABLE `general` DISABLE KEYS */;
INSERT INTO `general` (`id`, `time_when`, `login`, `text_msg`, `year_month_day`) VALUES (177,'11:19','BIGNIK','Есть чатик','20:9:1'),(178,'16:12','BIGNIK','ывоарыва','20:11:6');
/*!40000 ALTER TABLE `general` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `researchcosts`
--

DROP TABLE IF EXISTS `researchcosts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `researchcosts` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `base` text COLLATE utf8mb4_unicode_520_ci,
  `baseshtab` text COLLATE utf8mb4_unicode_520_ci,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `researchcosts`
--

LOCK TABLES `researchcosts` WRITE;
/*!40000 ALTER TABLE `researchcosts` DISABLE KEYS */;
INSERT INTO `researchcosts` (`id`, `base`, `baseshtab`) VALUES (1,'[{\"name\":\"MS1\",\"expPrice\":0,\"monPrice\":0,\"power\":4,\"type\":\"LT\",\"info\":\"Лёгкие танки могут перемещаться 2 раза за ход, причём могут переместиться 1 раз сразу после выхода на поле.\",\"damage\":1,\"hp\":4,\"plusFuel\":1,\"needFuel\":2,\"russianName\":\"МС-1\",\"nation\":\"USSR\"},{\"name\":\"BT2\",\"expPrice\":200,\"monPrice\":1600,\"power\":8,\"type\":\"LT\",\"info\":\"\\u0421\\u043e\\u0432\\u0435\\u0442\\u0441\\u043a\\u0438\\u0439 \\u043b\\u0435\\u0433\\u043a\\u0438\\u0439 \\u0442\\u0430\\u043d\\u043a 1 \\u0443\\u0440\\u043e\\u0432\\u043d\\u044f\",\"damage\":2,\"hp\":4,\"plusFuel\":2,\"needFuel\":3,\"russianName\":\"\\u0411\\u0422-2\"},{\"name\":\"T26\",\"expPrice\":250,\"monPrice\":2500,\"power\":8,\"type\":\"LT\",\"info\":\"\\u0421\\u043e\\u0432\\u0435\\u0442\\u0441\\u043a\\u0438\\u0439 \\u043b\\u0435\\u0433\\u043a\\u0438\\u0439 \\u0442\\u0430\\u043d\\u043a 1 \\u0443\\u0440\\u043e\\u0432\\u043d\\u044f\",\"damage\":2,\"hp\":6,\"plusFuel\":0,\"needFuel\":4,\"russianName\":\"\\u0422-26\"}, {\"name\":\"T6\",\"expPrice\":0,\"monPrice\":0,\"power\":5,\"type\":\"MT\",\"info\":\"Средние танки - самый универсальный тип техники. Могут перемещаться по диагонали.\",\"damage\":2,\"hp\":7,\"plusFuel\":1,\"needFuel\":5,\"russianName\":\"T6\",\"nation\":\"USA\"}, {\"name\":\"leichttraktor\",\"expPrice\":0,\"monPrice\":0,\"power\":3,\"type\":\"LT\",\"info\":\"Лёгкие танки могут перемещаться 2 раза за ход, причём могут переместиться 1 раз сразу после выхода на поле.\",\"damage\":2,\"hp\":2,\"plusFuel\":0,\"needFuel\":1,\"russianName\":\"Leichttraktor\",\"nation\":\"Germany\"},{\"name\":\"uo_crushprussian\",\"expPrice\":0,\"monPrice\":0,\"power\":0,\"type\":\"OC\",\"info\":\"Нанесите 2 повреждения выбранному штабу, технике или взводу.\",\"damage\":2,\"hp\":0,\"plusFuel\":0,\"needFuel\":2,\"russianName\":\"Heart of the Enemy\",\"nation\":\"USA\",\"numSpecial\":1}, {\"name\":\"sv_t24\",\"expPrice\":0,\"monPrice\":0,\"power\":5,\"type\":\"MT\",\"info\":\"Средние танки — самый универсальный тип техники. Могут перемещаться по диагонали.\",\"damage\":1,\"hp\":9,\"plusFuel\":0,\"needFuel\":5,\"russianName\":\"Т-24\",\"nation\":\"USSR\",\"numSpecial\":0},{\"name\":\"gv_t21\",\"expPrice\":0,\"monPrice\":0,\"power\":4,\"type\":\"MT\",\"info\":\"Средние танки — самый универсальный тип техники. Могут перемещаться по диагонали.\",\"damage\":3,\"hp\":5,\"plusFuel\":1,\"needFuel\":5,\"russianName\":\"T-21\",\"nation\":\"Germany\",\"numSpecial\":0}, {\"name\":\"so_budbditelnym\",\"expPrice\":0,\"monPrice\":0,\"power\":1,\"type\":\"OC\",\"info\":\"Возьмите карту.\\n\\nВосстановите 2 прочности вашему штабу.\",\"damage\":0,\"hp\":0,\"plusFuel\":0,\"needFuel\":2,\"russianName\":\"Помощь фронту\",\"nation\":\"USSR\",\"numSpecial\":2},{\"name\":\"go_tagderwehrmacht\",\"expPrice\":0,\"monPrice\":0,\"power\":0,\"type\":\"OC\",\"info\":\"Нанесите 2 повреждения выбранной технике.\\n\\nВосстановите 2 прочности вашему штабу.\",\"damage\":0,\"hp\":0,\"plusFuel\":0,\"needFuel\":4,\"russianName\":\"Mit jeder Schlacht\",\"nation\":\"Germany\",\"numSpecial\":3},{\"name\":\"uv_t7combatcar\",\"expPrice\":0,\"monPrice\":0,\"power\":2,\"type\":\"LT\",\"info\":\"Лёгкие танки могут перемещаться 2 раза за ход, причём могут переместиться 1 раз сразу после выхода на поле.\",\"damage\":3,\"hp\":1,\"plusFuel\":0,\"needFuel\":2,\"russianName\":\"T7 Combat Car\",\"nation\":\"USA\",\"numSpecial\":0},{\"name\":\"uv_t1lt\",\"expPrice\":0,\"monPrice\":0,\"power\":3,\"type\":\"LT\",\"info\":\"Лёгкие танки могут перемещаться 2 раза за ход, причём могут переместиться 1 раз сразу после выхода на поле.\",\"damage\":3,\"hp\":2,\"plusFuel\":1,\"needFuel\":3,\"russianName\":\"T1 LT\",\"nation\":\"USA\",\"numSpecial\":0}]','[{\"name\":\"sh_uchebnayachast\",\"expPrice\":0,\"monPrice\":0,\"info0\":\"Учебная часть\",\"info1\":\"Советский учебный штаб 1 уровня\",\"info2\":\"Учебная колода СССР\",\"power\":20,\"hp\":22,\"damage\":1,\"plusFuel\":5, \"nation\":\"USSR\"}, {\"name\":\"gh_trainingslager\",\"expPrice\":0,\"monPrice\":0,\"info0\":\"Trainingslager\",\"info1\":\"Немецкий учебный штаб 1 уровня\",\"info2\":\"Учебная колода Германии\",\"power\":20,\"hp\":16,\"damage\":2,\"plusFuel\":4,\"nation\":\"Germany\"}, {\"name\":\"uh_trainingcamp\",\"expPrice\":0,\"monPrice\":0,\"info0\":\"Tr. Camp\",\"info1\":\"Американский учебный штаб 1 уровня\",\"info2\":\"Учебная колода США\",\"power\":20,\"hp\":16,\"damage\":1,\"plusFuel\":6, \"nation\":\"USA\"}]');
/*!40000 ALTER TABLE `researchcosts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tablefindenemy`
--

DROP TABLE IF EXISTS `tablefindenemy`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tablefindenemy` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `login` varchar(191) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `status` int(11) unsigned DEFAULT NULL,
  `who_find_me` varchar(191) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `main_info` varchar(191) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `uniq_identity` varchar(191) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `first_step` int(11) unsigned DEFAULT NULL,
  `enemy_ready` tinyint(1) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=228 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tablefindenemy`
--

LOCK TABLES `tablefindenemy` WRITE;
/*!40000 ALTER TABLE `tablefindenemy` DISABLE KEYS */;
INSERT INTO `tablefindenemy` (`id`, `login`, `status`, `who_find_me`, `main_info`, `uniq_identity`, `first_step`, `enemy_ready`) VALUES (18,'Jopka',1,'BIGNIK','{\"countOfCard\":9}','BIGNIKJopka126117464',0,0);
/*!40000 ALTER TABLE `tablefindenemy` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `login` varchar(191) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `password` varchar(191) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `money` int(11) unsigned DEFAULT NULL,
  `expi` double DEFAULT NULL,
  `gold` int(11) unsigned DEFAULT NULL,
  `info_tanks` varchar(1000) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `shtabs` text COLLATE utf8mb4_unicode_520_ci,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` (`id`, `login`, `password`, `money`, `expi`, `gold`, `info_tanks`, `shtabs`) VALUES (30,'BIGNIK','77799931',62400,675,50,'{\"units\":[{\"name\":\"T6\",\"count\":3},{\"name\":\"leichttraktor\",\"count\":3},{\"name\":\"uo_crushprussian\",\"count\":3},{\"name\":\"sv_t24\",\"count\":3},{\"name\":\"gv_t21\",\"count\":3},{\"name\":\"MS1\",\"count\":3},{\"name\":\"so_budbditelnym\",\"count\":3},{\"name\":\"go_tagderwehrmacht\",\"count\":3},{\"name\":\"uv_t7combatcar\",\"count\":3},{\"name\":\"uv_t1lt\",\"count\":3}]}','{\"shtabs\":[{\"name\":\"sh_uchebnayachast\",\"power\":25,\"exp\":0,\"cards\":[{\"name\":\"uv_t7combatcar\",\"count\":3}]},{\"name\":\"gh_trainingslager\",\"power\":22,\"exp\":0,\"cards\":[{\"name\":\"T6\",\"count\":3},{\"name\":\"leichttraktor\",\"count\":3},{\"name\":\"uo_crushprussian\",\"count\":3},{\"name\":\"sv_t24\",\"count\":3},{\"name\":\"gv_t21\",\"count\":3},{\"name\":\"MS1\",\"count\":3},{\"name\":\"so_budbditelnym\",\"count\":3},{\"name\":\"go_tagderwehrmacht\",\"count\":3},{\"name\":\"uv_t7combatcar\",\"count\":3},{\"name\":\"uv_t1lt\",\"count\":3}]},{\"name\":\"uh_trainingcamp\",\"power\":23,\"exp\":0,\"cards\":[{\"name\":\"T6\",\"count\":3},{\"name\":\"leichttraktor\",\"count\":3},{\"name\":\"uo_crushprussian\",\"count\":3},{\"name\":\"sv_t24\",\"count\":3},{\"name\":\"gv_t21\",\"count\":3},{\"name\":\"MS1\",\"count\":3},{\"name\":\"so_budbditelnym\",\"count\":3},{\"name\":\"go_tagderwehrmacht\",\"count\":3},{\"name\":\"uv_t7combatcar\",\"count\":3},{\"name\":\"uv_t1lt\",\"count\":3}]}]}'),(34,'Matvey','12345678',200,550,50,'{\"units\":[{\"name\":\"MS1\",\"count\":3,\"idOfEverything\":0},{\"name\":\"BT2\",\"count\":3,\"idOfEverything\":0},{\"name\":\"T26\",\"count\":2,\"idOfEverything\":0}]}','{\"shtabs\":[{\"name\":\"sh_uchebnayachast\",\"power\":25,\"exp\":0,\"cards\":[{\"name\":\"MS1\",\"countCollection\":0,\"countColoda\":3},{\"name\":\"BT2\",\"countCollection\":3,\"countColoda\":0},{\"name\":\"T26\",\"countCollection\":0,\"countColoda\":2}]}]}'),(35,'DeafaulProfile','Passwd',0,0,0,'{\"units\":[{\"name\":\"T6\",\"count\":3},{\"name\":\"leichttraktor\",\"count\":3},{\"name\":\"uo_crushprussian\",\"count\":3},{\"name\":\"sv_t24\",\"count\":3},{\"name\":\"gv_t21\",\"count\":3},{\"name\":\"MS1\",\"count\":3},{\"name\":\"so_budbditelnym\",\"count\":3},{\"name\":\"go_tagderwehrmacht\",\"count\":3},{\"name\":\"uv_t7combatcar\",\"count\":3},{\"name\":\"uv_t1lt\",\"count\":3}]}','{\"shtabs\":[{\"name\":\"sh_uchebnayachast\",\"power\":25,\"exp\":0,\"cards\":[{\"name\":\"T6\",\"count\":3},{\"name\":\"leichttraktor\",\"count\":3},{\"name\":\"uo_crushprussian\",\"count\":3},{\"name\":\"sv_t24\",\"count\":3},{\"name\":\"gv_t21\",\"count\":3},{\"name\":\"MS1\",\"count\":3},{\"name\":\"so_budbditelnym\",\"count\":3},{\"name\":\"go_tagderwehrmacht\",\"count\":3},{\"name\":\"uv_t7combatcar\",\"count\":3},{\"name\":\"uv_t1lt\",\"count\":3}]},{\"name\":\"gh_trainingslager\",\"power\":22,\"exp\":0,\"cards\":[]},{\"name\":\"uh_trainingcamp\",\"power\":23,\"exp\":0,\"cards\":[]}]}'),(36,'BIGNEW','12345',62400,675,50,'{\"units\":[{\"name\":\"MS1\",\"count\":3,\"idOfEverything\":0},{\"name\":\"BT2\",\"count\":3,\"idOfEverything\":0},{\"name\":\"T26\",\"count\":3,\"idOfEverything\":0}]}','{\"shtabs\":[{\"name\":\"sh_uchebnayachast\",\"power\":25,\"exp\":0,\"cards\":[]},{\"name\":\"gh_trainingslager\",\"power\":22,\"exp\":0,\"cards\":[]},{\"name\":\"uh_trainingcamp\",\"power\":23,\"exp\":0,\"cards\":[]}]}'),(37,'Gena','228',62400,675,50,'{\"units\":[{\"name\":\"T6\",\"count\":3},{\"name\":\"leichttraktor\",\"count\":3},{\"name\":\"uo_crushprussian\",\"count\":3}]}','{\"shtabs\":[{\"name\":\"sh_uchebnayachast\",\"power\":25,\"exp\":0,\"cards\":[{\"name\":\"T6\",\"count\":3},{\"name\":\"leichttraktor\",\"count\":3},{\"name\":\"uo_crushprussian\",\"count\":3}]},{\"name\":\"gh_trainingslager\",\"power\":22,\"exp\":0,\"cards\":[{\"name\":\"uv_t7combatcar\",\"count\":3}]},{\"name\":\"uh_trainingcamp\",\"power\":23,\"exp\":0,\"cards\":[{\"name\":\"T6\",\"count\":3},{\"name\":\"leichttraktor\",\"count\":3},{\"name\":\"uo_crushprussian\",\"count\":3}]}]}');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `warhistory`
--

DROP TABLE IF EXISTS `warhistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `warhistory` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(191) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `step` double DEFAULT NULL,
  `login` varchar(191) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `history` varchar(191) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `end_flag` tinyint(1) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2464 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `warhistory`
--

LOCK TABLES `warhistory` WRITE;
/*!40000 ALTER TABLE `warhistory` DISABLE KEYS */;
INSERT INTO `warhistory` (`id`, `name`, `step`, `login`, `history`, `end_flag`) VALUES (1994,'GenaBIGNIK884819691',-1,'Gena','',0),(1995,'BIGNIKGena496373219',-1,'BIGNIK','',0),(1996,'BIGNIKGena166775116',-1,'BIGNIK','',0),(1997,'BIGNIKGena166775116',0,'BIGNIK','{\"whoAttack\":-1,\"toAttack\":6,\"isCardMove\":false,\"cardThatUse\":\"T6\"}',0),(1998,'BIGNIKGena508136473',-1,'BIGNIK','',0),(1999,'BIGNIKGena396303140',-1,'BIGNIK','',0),(2003,'BIGNIKGena281595698',-1,'BIGNIK','',0),(2013,'BIGNIKGena52123660',-1,'BIGNIK','',0),(2014,'BIGNIKGena378663689',-1,'BIGNIK','',0),(2019,'BIGNIKGena829215945',-1,'BIGNIK','',0),(2025,'BIGNIKGena933414729',-1,'BIGNIK','',0),(2030,'BIGNIKGena691803855',-1,'BIGNIK','',0),(2038,'BIGNIKGena522117455',-1,'BIGNIK','',0),(2039,'BIGNIKGena916928319',-1,'BIGNIK','',0),(2051,'BIGNIKGena26717358',-1,'BIGNIK','',0),(2052,'BIGNIKGena922006984',-1,'BIGNIK','',0),(2058,'BIGNIKGena227477090',-1,'BIGNIK','',0),(2070,'BIGNIKGena845231239',-1,'BIGNIK','',0),(2076,'BIGNIKGena875963978',-1,'BIGNIK','',0),(2088,'BIGNIKGena267150161',-1,'BIGNIK','',0),(2110,'GenaBIGNIK832575566',-1,'Gena','',0),(2143,'BIGNIKGena593156974',-1,'BIGNIK','',0),(2145,'BIGNIKGena593156974',1,'BIGNIK','{\"whoAttack\":-1,\"toAttack\":6,\"isCardMove\":false,\"cardThatUse\":\"sv_t24\"}',0),(2146,'BIGNIKGena400228004',-1,'BIGNIK','',0),(2153,'BIGNIKGena832941535',-1,'BIGNIK','',0),(2159,'BIGNIKGena123867866',-1,'BIGNIK','',0),(2175,'BIGNIKGena902493364',-1,'BIGNIK','',0),(2176,'BIGNIKGena977391001',-1,'BIGNIK','',0),(2182,'BIGNIKGena761477052',-1,'BIGNIK','',0),(2183,'BIGNIKGena679801045',-1,'BIGNIK','',0),(2184,'BIGNIKGena773069513',-1,'BIGNIK','',0),(2185,'BIGNIKGena95355080',-1,'BIGNIK','',0),(2186,'BIGNIKGena296150436',-1,'BIGNIK','',0),(2189,'BIGNIKGena42505172',-1,'BIGNIK','',0),(2200,'BIGNIKGena449721643',-1,'BIGNIK','',0),(2206,'BIGNIKGena722266998',-1,'BIGNIK','',0),(2211,'BIGNIKGena358974010',-1,'BIGNIK','',0),(2216,'BIGNIKGena26039961',-1,'BIGNIK','',0),(2226,'BIGNIKGena426575046',-1,'BIGNIK','',0),(2232,'BIGNIKGena596383520',-1,'BIGNIK','',0),(2237,'BIGNIKGena357891789',-1,'BIGNIK','',0),(2242,'BIGNIKGena16158076',-1,'BIGNIK','',0),(2251,'BIGNIKGena296172681',-1,'BIGNIK','',0),(2263,'GenaBIGNIK760818657',-1,'Gena','',0),(2268,'BIGNIKGena470566774',-1,'BIGNIK','',0),(2274,'BIGNIKGena35610270',-1,'BIGNIK','',0),(2289,'BIGNIKGena409938845',-1,'BIGNIK','',0),(2298,'BIGNIKGena176538447',-1,'BIGNIK','',0),(2304,'BIGNIKGena431675372',-1,'BIGNIK','',0),(2317,'BIGNIKGena481660924',-1,'BIGNIK','',0),(2335,'BIGNIKGena799030794',-1,'BIGNIK','',0),(2345,'BIGNIKGena909104294',-1,'BIGNIK','',0),(2363,'BIGNIKGena254706251',-1,'BIGNIK','',0),(2372,'BIGNIKGena190902478',-1,'BIGNIK','',0),(2382,'BIGNIKGena183060984',-1,'BIGNIK','',0),(2389,'BIGNIKGena464910769',-1,'BIGNIK','',0),(2396,'BIGNIKGena940202982',-1,'BIGNIK','',0),(2402,'BIGNIKGena582953569',-1,'BIGNIK','',0),(2409,'BIGNIKGena264148289',-1,'BIGNIK','',0),(2415,'BIGNIKGena239745035',-1,'BIGNIK','',0),(2421,'BIGNIKGena981772617',-1,'BIGNIK','',0),(2428,'BIGNIKGena905857713',-1,'BIGNIK','',0),(2435,'BIGNIKGena432617242',-1,'BIGNIK','',0),(2437,'BIGNIKGena432617242',1,'BIGNIK','{\"whoAttack\":-1,\"toAttack\":-1,\"isCardMove\":false,\"cardThatUse\":\"NULL\"}',1),(2438,'BIGNIKGena353466541',-1,'BIGNIK','',0),(2440,'BIGNIKGena353466541',1,'BIGNIK','{\"whoAttack\":-1,\"toAttack\":-1,\"isCardMove\":false,\"cardThatUse\":\"NULL\"}',1),(2441,'BIGNIKGena808171197',-1,'BIGNIK','',0),(2444,'BIGNIKGena119636718',-1,'BIGNIK','',0),(2446,'BIGNIKGena119636718',1,'BIGNIK','{\"whoAttack\":-1,\"toAttack\":-1,\"isCardMove\":false,\"cardThatUse\":\"NULL\"}',1),(2447,'BIGNIKGena366377603',-1,'BIGNIK','',0),(2450,'BIGNIKGena696599852',-1,'BIGNIK','',0),(2462,'BIGNIKGena696599852',11,'BIGNIK','{\"whoAttack\":6,\"toAttack\":7,\"isCardMove\":true,\"cardThatUse\":\"NULL\"}',0),(2463,'BIGNIKGena696599852',12,'BIGNIK','{\"whoAttack\":-1,\"toAttack\":-1,\"isCardMove\":false,\"cardThatUse\":\"NULL\"}',1);
/*!40000 ALTER TABLE `warhistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'n68203_mainBase'
--

--
-- Dumping routines for database 'n68203_mainBase'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-02-03  6:21:41
