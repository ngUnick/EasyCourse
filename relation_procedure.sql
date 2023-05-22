CREATE DEFINER=`iee2019067`@`%` PROCEDURE `new_procedure`()
BEGIN
set @i = 1;
set @j = 1;
while @i <= 15168 do
update modules
 SET ModuleLeader = @j
 where idModules = @i;
set @i=@i+1;
if @j > 1021 then
set @j=0;
end if;
set @j=@j+1;
END WHILE;
END