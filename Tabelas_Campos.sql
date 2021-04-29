/*
 Navicat Premium Data Transfer

 Source Server         : PI2
 Source Server Type    : MySQL
 Source Server Version : 80023
 Source Host           : 200.132.195.95:3306
 Source Schema         : AssessoriaWeb

 Target Server Type    : MySQL
 Target Server Version : 80023
 File Encoding         : 65001

 Date: 28/04/2021 21:46:04

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for Assessor
-- ----------------------------
DROP TABLE IF EXISTS `Assessor`;
CREATE TABLE `Assessor`  (
  `ass_id` int NOT NULL AUTO_INCREMENT,
  `ass_tipo` varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `pes_id` int NULL DEFAULT NULL,
  PRIMARY KEY (`ass_id`) USING BTREE,
  INDEX `fk_ass_pes_id_idx`(`pes_id`) USING BTREE,
  CONSTRAINT `fk_ass_pes_id` FOREIGN KEY (`pes_id`) REFERENCES `Pessoa` (`pes_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for Atividade
-- ----------------------------
DROP TABLE IF EXISTS `Atividade`;
CREATE TABLE `Atividade`  (
  `ati_id` int NOT NULL AUTO_INCREMENT,
  `ati_descricao` varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `atl_observacao` varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `tre_id` int NULL DEFAULT NULL,
  PRIMARY KEY (`ati_id`) USING BTREE,
  INDEX `fk_ati_tre_id_idx`(`tre_id`) USING BTREE,
  CONSTRAINT `fk_ati_tre_id` FOREIGN KEY (`tre_id`) REFERENCES `Treinamento` (`tre_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for AtividadeTreinamento
-- ----------------------------
DROP TABLE IF EXISTS `AtividadeTreinamento`;
CREATE TABLE `AtividadeTreinamento`  (
  `ati_id` int NOT NULL,
  `tre_id` int NOT NULL,
  INDEX `fk_ati_tre_ati_id_idx`(`ati_id`) USING BTREE,
  INDEX `fk_ati_tre_tre_id_idx`(`tre_id`) USING BTREE,
  CONSTRAINT `fk_ati_tre_ati_id` FOREIGN KEY (`ati_id`) REFERENCES `Atividade` (`ati_id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_ati_tre_tre_id` FOREIGN KEY (`tre_id`) REFERENCES `Treinamento` (`tre_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for Atleta
-- ----------------------------
DROP TABLE IF EXISTS `Atleta`;
CREATE TABLE `Atleta`  (
  `atl_id` int NOT NULL AUTO_INCREMENT,
  `atl_categoria` varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `atl_observacao` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `pes_id` int NULL DEFAULT NULL,
  PRIMARY KEY (`atl_id`) USING BTREE,
  INDEX `fk_atl_pes_id_idx`(`pes_id`) USING BTREE,
  CONSTRAINT `fk_atl_pes_id` FOREIGN KEY (`pes_id`) REFERENCES `Pessoa` (`pes_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for AtletaTurma
-- ----------------------------
DROP TABLE IF EXISTS `AtletaTurma`;
CREATE TABLE `AtletaTurma`  (
  `atl_id` int NULL DEFAULT NULL,
  `trm_id` int NULL DEFAULT NULL,
  INDEX `fk_atltrm_atl_id_idx`(`atl_id`) USING BTREE,
  INDEX `fk_atltrm_trm_id_idx`(`trm_id`) USING BTREE,
  CONSTRAINT `fk_atltrm_atl_id` FOREIGN KEY (`atl_id`) REFERENCES `Atleta` (`atl_id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_atltrm_trm_id` FOREIGN KEY (`trm_id`) REFERENCES `Turma` (`trm_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for Avaliacao
-- ----------------------------
DROP TABLE IF EXISTS `Avaliacao`;
CREATE TABLE `Avaliacao`  (
  `ava_id` int NOT NULL AUTO_INCREMENT,
  `ava_data` date NULL DEFAULT NULL,
  `ava_peso` float NULL DEFAULT NULL,
  `ava_gorduraVisceral` float NULL DEFAULT NULL,
  `ava_gorduraCorporal` float NULL DEFAULT NULL,
  `ava_bioimpedancia` float NULL DEFAULT NULL,
  `atl_id` int NULL DEFAULT NULL,
  PRIMARY KEY (`ava_id`) USING BTREE,
  INDEX `fk_ava_atl_id_idx`(`atl_id`) USING BTREE,
  CONSTRAINT `fk_ava_atl_id` FOREIGN KEY (`atl_id`) REFERENCES `Atleta` (`atl_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for Endereco
-- ----------------------------
DROP TABLE IF EXISTS `Endereco`;
CREATE TABLE `Endereco`  (
  `end_id` int NOT NULL AUTO_INCREMENT,
  `end_bairro` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `end_numero` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `end_logradouro` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `end_complemento` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `end_cep` varchar(20) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `pes_id` int NULL DEFAULT NULL,
  PRIMARY KEY (`end_id`) USING BTREE,
  INDEX `fk_end_pes_id_idx`(`pes_id`) USING BTREE,
  CONSTRAINT `fk_end_pes_id` FOREIGN KEY (`pes_id`) REFERENCES `Pessoa` (`pes_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for Nutricionista
-- ----------------------------
DROP TABLE IF EXISTS `Nutricionista`;
CREATE TABLE `Nutricionista`  (
  `nut_id` int NOT NULL AUTO_INCREMENT,
  `nut_crn` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `nut_cnpj` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `pes_id` int NULL DEFAULT NULL,
  PRIMARY KEY (`nut_id`) USING BTREE,
  INDEX `fk_nut_pes_id_idx`(`pes_id`) USING BTREE,
  CONSTRAINT `fk_nut_pes_id` FOREIGN KEY (`pes_id`) REFERENCES `Pessoa` (`pes_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for Pessoa
-- ----------------------------
DROP TABLE IF EXISTS `Pessoa`;
CREATE TABLE `Pessoa`  (
  `pes_id` int NOT NULL AUTO_INCREMENT,
  `pes_nome` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `pes_cpf` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `pes_email` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `pes_datanascimento` date NULL DEFAULT NULL,
  `pes_telefone` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `pes_sexo` char(8) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `pes_login` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL,
  `pes_senha` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL,
  `tpp_id` int NULL DEFAULT NULL,
  PRIMARY KEY (`pes_id`) USING BTREE,
  INDEX `pes_tipo_pessoa`(`tpp_id`) USING BTREE,
  CONSTRAINT `pes_tipo_pessoa` FOREIGN KEY (`tpp_id`) REFERENCES `TipoPessoa` (`tpp_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Table structure for PlanoAlimentar
-- ----------------------------
DROP TABLE IF EXISTS `PlanoAlimentar`;
CREATE TABLE `PlanoAlimentar`  (
  `pln_id` int NOT NULL AUTO_INCREMENT,
  `pln_datainicial` date NULL DEFAULT NULL,
  `pln_datafinal` date NULL DEFAULT NULL,
  `pla_plano` varchar(5000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `atl_id` int NULL DEFAULT NULL,
  `nut_id` int NULL DEFAULT NULL,
  PRIMARY KEY (`pln_id`) USING BTREE,
  INDEX `fk_pln_nut_id_idx`(`nut_id`) USING BTREE,
  INDEX `fk_pln_atl_id_idx`(`atl_id`) USING BTREE,
  CONSTRAINT `fk_pln_atl_id` FOREIGN KEY (`atl_id`) REFERENCES `Atleta` (`atl_id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_pln_nut_id` FOREIGN KEY (`nut_id`) REFERENCES `Nutricionista` (`nut_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for TipoPessoa
-- ----------------------------
DROP TABLE IF EXISTS `TipoPessoa`;
CREATE TABLE `TipoPessoa`  (
  `tpp_id` int NOT NULL AUTO_INCREMENT,
  `tpp_descricao` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  PRIMARY KEY (`tpp_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for Treinamento
-- ----------------------------
DROP TABLE IF EXISTS `Treinamento`;
CREATE TABLE `Treinamento`  (
  `tre_id` int NOT NULL AUTO_INCREMENT,
  `tre_data` date NULL DEFAULT NULL,
  `tre_hora` time(0) NULL DEFAULT NULL,
  `tre_valor` float NULL DEFAULT NULL,
  `ass_id` int NULL DEFAULT NULL,
  `atl_id` int NULL DEFAULT NULL,
  PRIMARY KEY (`tre_id`) USING BTREE,
  INDEX `fk_tre_ass_id_idx`(`ass_id`) USING BTREE,
  INDEX `fk_tre_atl_id_idx`(`atl_id`) USING BTREE,
  CONSTRAINT `fk_tre_ass_id` FOREIGN KEY (`ass_id`) REFERENCES `Assessor` (`ass_id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tre_atl_id` FOREIGN KEY (`atl_id`) REFERENCES `Atleta` (`atl_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for Turma
-- ----------------------------
DROP TABLE IF EXISTS `Turma`;
CREATE TABLE `Turma`  (
  `trm_id` int NOT NULL AUTO_INCREMENT,
  `trm_descricao` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `trm_observacao` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `trm_HoraInicial` time(0) NULL DEFAULT NULL,
  `trm_HoraFinal` time(0) NULL DEFAULT NULL,
  `ass_id` int NULL DEFAULT NULL,
  PRIMARY KEY (`trm_id`) USING BTREE,
  INDEX `fk_trm_ass_id_idx`(`ass_id`) USING BTREE,
  CONSTRAINT `fk_trm_ass_id` FOREIGN KEY (`ass_id`) REFERENCES `Assessor` (`ass_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
*/
