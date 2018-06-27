select 
a.GCustName as SourceName
,a.gTel as Source
,'56' as IndependentSource
,a.BillDate as SourceCreateDate
,GETDATE() as SourceCollectionDate
,'{GoodsName:"'+a.GoodsName+'"}' as SourceRemark
,a.EndAdd as Area

from CompactIndex a
where 1=1
and a.BillDate <= '2018-06-27'
;