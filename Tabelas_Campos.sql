/*ativa sensitive case no login e na senha*/
ALTER TABLE Pessoa MODIFY pes_login varchar(50) COLLATE utf8_bin;
ALTER TABLE Pessoa MODIFY pes_senha varchar(50) COLLATE utf8_bin;