select 
a.SenderUnit as SourceName
,a.SenderTel as Source
,'56' as IndependentSource
,a.EndDate as SourceCreateDate
,GETDATE() as SourceCollectionDate
,'{GoodsName:"'+a.GoodsName+'"}' as SourceRemark
,b.BeginAdd as Area

from CompactIndex b
left join CompactInfo a
on a.ID = b.ID
where b.BillDate <='2018-06-27'
;