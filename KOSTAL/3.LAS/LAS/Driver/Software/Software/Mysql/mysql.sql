CREATE DATABASE vet;
USE vet;
DROP TABLE IF EXISTS `groups`;
CREATE TABLE `groups` (`Name` varchar(20) NOT NULL default '',
  `Statistics` tinyint(1) NOT NULL default '0',
  `Test` tinyint(1) NOT NULL default '0',
  `Update` tinyint(1) NOT NULL default '0',
  `Calibration` tinyint(1) NOT NULL default '0',
  `debug` tinyint(1) NOT NULL default '0',
  PRIMARY KEY  (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Group for users';
INSERT INTO `groups` (`Name`,`Statistics`,`Test`,`Update`,`Calibration`,`debug`) VALUES 
 ('APBEngineer',1,1,1,1,1),
 ('APMAdministator',1,1,0,0,0),
 ('Operator',0,1,0,0,0);
DROP TABLE IF EXISTS `statistics`;
CREATE TABLE `statistics` (
  `ArticleNo` varchar(50) NOT NULL default '',
  `Pass` int(10) unsigned NOT NULL default '0',
  `Fail` int(10) unsigned NOT NULL default '0',
  `SN` int(10) unsigned NOT NULL default '0',
  `LastDate` datetime NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`ArticleNo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
DROP TABLE IF EXISTS `tb_articlenumber`;
CREATE TABLE `tb_articlenumber` (
  `ArticleNumb` varchar(50) NOT NULL default '',
  `TestDate` datetime NOT NULL default '0000-00-00 00:00:00',
  `TestID` varchar(45) NOT NULL default '',
  PRIMARY KEY  (`TestID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
DROP TABLE IF EXISTS `tb_parameter`;
CREATE TABLE `tb_parameter` (
  `ArticleNumb` varchar(50) NOT NULL default '',
  `TestNr` varchar(10) NOT NULL default '',
  `LowLimit` varchar(50) default NULL,
  `UpLimit` varchar(50) default NULL,
  `Mask` varchar(50) default NULL,
  `Remark` varchar(100) default NULL,
  `Unit` varchar(10) default NULL,
  `Version` int(10) unsigned NOT NULL default '0',
  `LimitID` varchar(6) NOT NULL default '',
  `Nomvalue` varchar(45) default NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
DROP TABLE IF EXISTS `tb_result`;
CREATE TABLE `tb_result` (
  `ArticalNumb` varchar(50) NOT NULL default '',
  `TestNr` varchar(10) NOT NULL default '',
  `ResultStr` varchar(50) NOT NULL default '',
  `TestID` varchar(20) NOT NULL default '',
  `ParameterVersion` int(10) unsigned NOT NULL default '0',
  `LimitID` varchar(6) NOT NULL default ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users` (
  `Name` varchar(20) NOT NULL default '',
  `Group` varchar(20) NOT NULL default '',
  `Password` varchar(20) default NULL,
  PRIMARY KEY  (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT=' 用户列表';
INSERT INTO `users` (`Name`,`Group`,`Password`) VALUES 
 ('apb','APBEngineer','apb34eol'),
 ('operator','Operator',NULL),
 ('administrator','APMAdministator','admin');
CREATE USER VET;
GRANT INSERT,UPDATE,SELECT ON vet.* to VET;   
SET PASSWORD FOR VET = PASSWORD("apb34eol");
