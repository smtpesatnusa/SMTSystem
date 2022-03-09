/*
SQLyog Community v13.1.6 (64 bit)
MySQL - 10.4.18-MariaDB : Database - aohai
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`aohai` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;

USE `aohai`;

/*Table structure for table `tbl_isn_list` */

DROP TABLE IF EXISTS `tbl_isn_list`;

CREATE TABLE `tbl_isn_list` (
  `Id` int(5) NOT NULL AUTO_INCREMENT,
  `Workorder` varchar(30) NOT NULL,
  `Marking` varchar(70) NOT NULL,
  `YuanSuanBan` varchar(17) NOT NULL,
  `WO_PTSN` varchar(50) DEFAULT NULL,
  `Batch` int(10) DEFAULT NULL,
  `Session` varchar(30) DEFAULT NULL,
  `Createdate` datetime DEFAULT NULL,
  `CreatedBy` varchar(20) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*Data for the table `tbl_isn_list` */

/*Table structure for table `tbl_model` */

DROP TABLE IF EXISTS `tbl_model`;

CREATE TABLE `tbl_model` (
  `id` int(5) NOT NULL AUTO_INCREMENT,
  `Modelno` varchar(30) NOT NULL,
  `type` varchar(30) NOT NULL,
  `CreateBy` varchar(20) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;

/*Data for the table `tbl_model` */

insert  into `tbl_model`(`id`,`Modelno`,`type`,`CreateBy`,`CreateDate`) values 
(1,'B0156HA13010','HASHBOARD','12345','2022-03-07 09:36:54'),
(2,'B0156HA12010','HASHBOARD','12345','2022-03-07 09:37:08'),
(3,'B0000BB26010','BeagleBone Black','12345','2022-03-07 09:38:42');

/*Table structure for table `tbl_pcblist` */

DROP TABLE IF EXISTS `tbl_pcblist`;

CREATE TABLE `tbl_pcblist` (
  `id` int(5) NOT NULL AUTO_INCREMENT,
  `woCust` varchar(30) NOT NULL,
  `pcbsn` varchar(70) NOT NULL,
  `marking` varchar(70) NOT NULL,
  `yuanSuanBan` varchar(17) NOT NULL,
  `woPtsn` varchar(30) NOT NULL,
  `substoreDate` datetime DEFAULT NULL,
  `substoreBy` varchar(20) DEFAULT NULL,
  `fgDate` datetime DEFAULT NULL,
  `fgBy` varchar(20) DEFAULT NULL,
  `batchSubstore` int(10) DEFAULT NULL,
  `batchFg` int(10) DEFAULT NULL,
  `session` varchar(30) DEFAULT NULL,
  `status` enum('substore','fg') DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*Data for the table `tbl_pcblist` */

/*Table structure for table `tbl_po` */

DROP TABLE IF EXISTS `tbl_po`;

CREATE TABLE `tbl_po` (
  `id` int(8) NOT NULL AUTO_INCREMENT,
  `PO_no` varchar(20) NOT NULL,
  `wo_Cust` varchar(30) NOT NULL,
  `model` varchar(50) NOT NULL,
  `qty` int(25) NOT NULL,
  `createdate` datetime NOT NULL,
  `createdBy` varchar(20) NOT NULL,
  `importDate` datetime DEFAULT NULL,
  `importBy` varchar(20) DEFAULT NULL,
  `status` enum('create','import','print','substore','fg') DEFAULT NULL,
  KEY `id` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*Data for the table `tbl_po` */

/*Table structure for table `tbl_user` */

DROP TABLE IF EXISTS `tbl_user`;

CREATE TABLE `tbl_user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(25) NOT NULL,
  `name` varchar(30) DEFAULT NULL,
  `pass` longtext NOT NULL,
  `role` varchar(25) NOT NULL,
  PRIMARY KEY (`id`,`username`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4;

/*Data for the table `tbl_user` */

insert  into `tbl_user`(`id`,`username`,`name`,`pass`,`role`) values 
(1,'039928','Yuninda Faranika','12345','AE'),
(2,'12345','Guest','12345','AE'),
(3,'037023','Achmad Budi Cahyono','Passw0rd','EA'),
(4,'38720','Jarowno','14022','EA'),
(5,'38724','Jefri','123','EA');

/*Table structure for table `tbl_woptsn` */

DROP TABLE IF EXISTS `tbl_woptsn`;

CREATE TABLE `tbl_woptsn` (
  `id` int(5) NOT NULL AUTO_INCREMENT,
  `WO_PTSN` varchar(30) NOT NULL,
  `qtyWO` varchar(30) NOT NULL,
  `PO_Customer` varchar(30) NOT NULL,
  `Modelno` varchar(30) NOT NULL,
  `WO_Customer` varchar(30) NOT NULL,
  `icCode` longtext NOT NULL,
  `createDate` datetime NOT NULL,
  `createBy` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*Data for the table `tbl_woptsn` */

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
