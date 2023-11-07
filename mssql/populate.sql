
-- Insert random data into market_items
DECLARE @i INT = 1
WHILE @i <= 100
BEGIN
    INSERT INTO market_items (name, description, meta_data)
    VALUES (
        'Item' + CAST(@i AS NVARCHAR(10)),
        'Description ' + CAST(NEWID() AS NVARCHAR(36)),
        'Meta Data ' + CAST(NEWID() AS NVARCHAR(36))
    )
    SET @i = @i + 1
END

-- Insert random data into auction
DECLARE @j INT = 1
WHILE @j <= 100
BEGIN
    INSERT INTO auction (item_id, created_at, finished_at, price, status, seller, buyer)
    VALUES (
        (SELECT TOP 1 id FROM market_items ORDER BY NEWID()), -- Randomly select an item_id
        DATEADD(SECOND, -ABS(CHECKSUM(NEWID())) % 31536000, GETDATE()), -- Random created_at date within the last year
        NULL, -- finished_at can be null
        CAST(ABS(CHECKSUM(NEWID())) % 1000 AS MONEY), -- Random price
        CAST(ABS(CHECKSUM(NEWID())) % 4 AS INT), -- Random status (0-3)
        'Seller ' + CAST(@j AS NVARCHAR(10)),
        'Buyer ' + CAST(@j AS NVARCHAR(10))
    )
    SET @j = @j + 1
END
