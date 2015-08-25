SELECT
  a.FinTransCodeId "TransactionCodeId",
  a.SiteId,
  a.TransactionCode,
  a.Description,
  a.Amount,
  a.GLAccount,
  b.Freq,
  CAST(b.Amt as decimal(18,2)) "AvgAmount",
  b.MaxAmt "MaxAmount"
FROM t_FinTransCode a
INNER JOIN (SELECT
  TransactionCodeId,
  COUNT(transactionCodeId) "Freq",
  AVG(Amount) "Amt",
  MAX(Amount) "MaxAmt"
FROM [TSWDATA].[dbo].[t_FolioLineItem]
WHERE TransactionDate > DateADD(yy, -2, CAST(CAST(YEAR(GETDATE()) AS varchar) + '-12-31' AS DATETIME))
AND Amount >= 0 AND SiteId = @SiteId
GROUP BY TransactionCodeId) b

  ON a.FinTransCodeId = b.TransactionCodeId
WHERE a.RoomCharge = 1
AND a.SiteId = @SiteId
ORDER BY a.TransactionCode