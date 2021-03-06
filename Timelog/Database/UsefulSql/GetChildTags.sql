﻿----------------------------------------------------------------
-- Child_Tags Function
----------------------------------------------------------------
IF OBJECT_ID (N'cfGetChildTags', N'TF') IS NOT NULL
    DROP FUNCTION cfGetChildTags;
GO

CREATE FUNCTION cfGetChildTags(@tagTreeId int) 
   RETURNS @retTable TABLE
   (
      id int not null,
	  tagId int not null,
      relatedTagTreeId int,
      level int not null
   )
AS
BEGIN

   if(@tagTreeId is not null)
   BEGIN
      with tagTrees (id, tagId, relatedTagTreeId, level) as
      (
         -- Anchor member definition
          select id, tagId, relatedTagTreeId, 0 as level 
          from TagTree t 
          where t.id = @tagTreeId
          union all
         -- Recursive member definition
          select t.id, t.tagId, t.relatedTagTreeId, level + 1 
          from  TagTree t
            join tagTrees s on s.id = t.relatedTagTreeId
      )
      INSERT @retTable
      select id, tagId, relatedTagTreeId, level
      from tagTrees
   END
   ELSE BEGIN
      --We have a null telco, so return all the telcos.
      with tagTrees (id, tagId, relatedTagTreeId, level) as
      (
         -- Anchor member definition
          select id, tagId, relatedTagTreeId, 0 as level 
          from TagTree t 
          where relatedTagTreeId is null
          union all
         -- Recursive member definition
          select t.id, t.tagId, t.relatedTagTreeId, level + 1 
          from  TagTree t
            join tagTrees s on s.id = t.relatedTagTreeId
      )
      INSERT @retTable
      select id, tagId, relatedTagTreeId, level
      from tagTrees
   END
   
   RETURN
end
GO