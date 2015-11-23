--Populating SQL server with Categories
insert into CategoryModels(Category)
values('Laptop');
insert into CategoryModels(Category)
values('Printer');
insert into CategoryModels(Category)
values('Computer');
insert into CategoryModels(Category)
values('Mouse');

--Populating SQL server with Models
insert into Models(Code,Name,Category,Description)
values('LT_00001', 'Dell XPS 13', 'Laptop', 'i7 quad core 32gb ram');

insert into Models(Code,Name,Category,Description)
values('LT_00002', 'Dell XPS 15', 'Laptop', 'i7 quad core 32gb ram');

insert into Models(Code,Name,Category,Description)
values('PC_00003', 'Dell Tower', 'Computer', 'i7 quad core GTX980ti SLI');

insert into Models(Code,Name,Category,Description)
values('PC_00004', 'Dell Server', 'Computer', 'Xeon (x2) quad core 128gb ram');

insert into Models(Code,Name,Category,Description)
values('PT_00005', 'HP LaserJet', 'Printer', '100ppm 900dpi');


SELECT * from CategoryModels 
SELECT * from Models

