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
	  tag_id int not null,
      relatedTagTree_Id int,
      level int not null
   )
AS
BEGIN

   if(@tagTreeId is not null)
   BEGIN
      with tagTrees (id, tag_id, relatedTagTree_Id, level) as
      (
         -- Anchor member definition
          select id, tag_id, relatedTagTree_Id, 0 as level 
          from TagTree t 
          where t.id = @tagTreeId
          union all
         -- Recursive member definition
          select t.id, t.tag_id, t.relatedTagTree_Id, level + 1 
          from  TagTree t
            join tagTrees s on s.id = t.relatedTagTree_Id
      )
      INSERT @retTable
      select id, tag_id, relatedTagTree_Id, level
      from tagTrees
   END
   ELSE BEGIN
      --We have a null telco, so return all the telcos.
      with tagTrees (id, tag_id, relatedTagTree_Id, level) as
      (
         -- Anchor member definition
          select id, tag_id, relatedTagTree_Id, 0 as level 
          from TagTree t 
          where relatedTagTree_Id is null
          union all
         -- Recursive member definition
          select t.id, t.tag_id, t.relatedTagTree_Id, level + 1 
          from  TagTree t
            join tagTrees s on s.id = t.relatedTagTree_Id
      )
      INSERT @retTable
      select id, tag_id, relatedTagTree_Id, level
      from tagTrees
   END
   
   RETURN
end
GO
