 CREATE DATABASE Data
 GO

 USE Data
 GO

-- Trạng thái bàn
CREATE TABLE TableStatus
(
	ID INT IDENTITY PRIMARY KEY,
	Status NVARCHAR(100) NOT NULL
)
GO

-- Bàn ăn
CREATE TABLE TableFood
(
	ID INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL,
	StatusID INT NOT NULL,
	
	FOREIGN KEY (StatusID) REFERENCES TableStatus(ID)
)
GO

-- Loại tài khoản
CREATE TABLE AccountType
(
	ID INT PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL
)
GO

-- Tài khoản
CREATE TABLE Account
(
	Username NVARCHAR(100) PRIMARY KEY,
	DisplayName NVARCHAR(100) NOT NULL DEFAULT N'Người dùng chưa đặt tên',
	Password NVARCHAR(1000) NOT NULL DEFAULT N'741253021220717864511724120418410161155', -- 0000
	TypeID INT NOT NULL,

	FOREIGN KEY (TypeID) REFERENCES dbo.AccountType(ID)
)
GO

-- Loại món ăn
CREATE TABLE FoodCategory
(
	ID INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL DEFAULT N'Loại món ăn chưa đặt tên'
)
GO

-- Món ăn
CREATE TABLE Food
(
	ID INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL DEFAULT N'Món ăn chưa đặt tên',
	CategoryID INT NOT NULL,
	Price FLOAT NOT NULL CHECK (Price >= 0) DEFAULT 0

	FOREIGN KEY (CategoryID) REFERENCES dbo.FoodCategory(ID)
)
GO

-- Hóa đơn
CREATE TABLE Bill
(
	ID INT IDENTITY PRIMARY KEY,
	DateCheckIn DATE DEFAULT GETDATE() NOT NULL,
	DateCheckOut DATE,
	TableID INT NOT NULL,
	Discount INT DEFAULT 0 NOT NULL,
	TotalPrice FLOAT DEFAULT 0 CHECK(TotalPrice >= 0),
	Status INT NOT NULL CHECK (Status IN (0, 1)) DEFAULT 0	-- 1: Đã thanh toán || 0: Chưa thanh toán

	FOREIGN KEY (TableID) REFERENCES dbo.TableFood(ID)
)
GO

-- Thông tin hóa đơn
CREATE TABLE BillInfo
(
	ID INT IDENTITY PRIMARY KEY,
	BillID INT NOT NULL,
	FoodID INT NOT NULL,
	Amount INT NOT NULL CHECK (Amount > 0) DEFAULT 1

	FOREIGN KEY(BillID) REFERENCES dbo.Bill(ID),
	FOREIGN KEY(FoodID) REFERENCES dbo.Food(ID)
)
GO

-- Thêm loại tài khoản
INSERT INTO dbo.AccountType (ID, Name) 
VALUES (1, N'Administrator')
INSERT INTO dbo.AccountType (ID, Name) 
VALUES (2, N'Staff')
GO

-- Thêm tài khoản
INSERT INTO dbo.Account (Username, DisplayName, Password, TypeID) 
VALUES (N'admin', N'Administrator', N'3244185981728979115075721453575112', 1) -- 123
GO

-- Thêm trạng thái bàn ăn
INSERT INTO dbo.TableStatus (Status) 
VALUES (N'Trống')
INSERT INTO dbo.TableStatus (Status) 
VALUES (N'Có người')
INSERT INTO dbo.TableStatus (Status) 
VALUES (N'Bị hỏng')
INSERT INTO dbo.TableStatus (Status) 
VALUES (N'Đã xóa')
GO

-- Hàm lấy mã trạng thái bàn theo trạng thái
CREATE FUNCTION UF_GetTableStatusID(@StatusName NVARCHAR(100))
RETURNS INT
AS
BEGIN
	DECLARE @statusID INT = -1
	SELECT @statusID = ID FROM TableStatus WHERE Status = @StatusName
	RETURN @statusID
END
GO

-- Thêm bàn ăn
DECLARE @statusID INT = dbo.UF_GetTableStatusID(N'Trống')
DECLARE @i INT = 1
WHILE @i <= 10
BEGIN
	INSERT dbo.TableFood (Name, StatusID) VALUES ( N'Bàn ' + CAST(@i AS NVARCHAR(100)), @statusID)
	SET @i = @i + 1
END
GO

-- Thêm Loại món
INSERT dbo.FoodCategory (Name) VALUES (N'Nước ngọt')
INSERT dbo.FoodCategory (Name) VALUES (N'Nước ép')
INSERT dbo.FoodCategory (Name) VALUES (N'Sinh tố')
INSERT dbo.FoodCategory (Name) VALUES (N'Cà phê')
INSERT dbo.FoodCategory (Name) VALUES (N'Trà')
INSERT dbo.FoodCategory (Name) VALUES (N'Thức ăn')
INSERT dbo.FoodCategory (Name) VALUES (N'Sữa')
GO

-- Thêm món ăn
INSERT dbo.Food (Name, CategoryID, Price) VALUES (N'Number 1', 1, 10000)
INSERT dbo.Food (Name, CategoryID, Price) VALUES (N'7Up', 1, 6000)
INSERT dbo.Food (Name, CategoryID, Price) VALUES (N'Nước mía', 2, 8000)
INSERT dbo.Food (Name, CategoryID, Price) VALUES (N'Nước cam', 2, 7000)
INSERT dbo.Food (Name, CategoryID, Price) VALUES (N'Sinh tố dâu', 3, 9000)
INSERT dbo.Food (Name, CategoryID, Price) VALUES (N'Cà phê sữa', 4, 15000)
INSERT dbo.Food (Name, CategoryID, Price) VALUES (N'Cà phê có đường', 4, 10000)
INSERT dbo.Food (Name, CategoryID, Price) VALUES (N'Trà đá', 5, 3000)
INSERT dbo.Food (Name, CategoryID, Price) VALUES (N'Trà đào', 5, 10000)
INSERT dbo.Food (Name, CategoryID, Price) VALUES (N'Bánh tráng trộn', 6, 12000)
INSERT dbo.Food (Name, CategoryID, Price) VALUES (N'Xoài lắc nước mắm', 6, 12000)
INSERT dbo.Food (Name, CategoryID, Price) VALUES (N'Bánh mì nướng', 6, 20000)
INSERT dbo.Food (Name, CategoryID, Price) VALUES (N'Cá viên chiên', 6, 8000)
INSERT dbo.Food (Name, CategoryID, Price) VALUES (N'Sữa Cô gái Hà Lan', 7, 8000)
INSERT dbo.Food (Name, CategoryID, Price) VALUES (N'Sữa Ông Thọ', 7, 8000)
GO

-- Thủ tục thêm món ăn cho hóa đơn
CREATE PROC USP_InsertBillInfo
@tableID INT, @foodID INT, @amount INT
AS
BEGIN
	-- Tìm hóa đơn chưa thanh toán của bàn thêm món ăn
	DECLARE @billID INT = -1
	SELECT @billID = ID FROM dbo.Bill WHERE TableID = @tableID AND Status = 0

	-- Nếu chưa có hóa đơn thì tạo
	IF(@billID = -1)
	BEGIN
		INSERT INTO Bill (DateCheckIn, DateCheckOut, TableID, Discount, Status) 
		VALUES (GETDATE(), null, @tableID, 0, 0)
		SELECT @billID = ID FROM dbo.Bill WHERE TableID = @tableID AND Status = 0
	END

	DECLARE @isExistBillInfo INT = -1
	DECLARE @foodAmount INT = 0

	SELECT @isExistBillInfo = ID, @foodAmount = Amount
	FROM BillInfo
	WHERE BillID = @billID AND FoodID = @foodID

	-- Nếu món ăn chưa được thêm trước đó
	IF(@isExistBillInfo = -1)
	BEGIN
		INSERT dbo.BillInfo (BillID, FoodID, Amount) VALUES (@billID, @foodID, @amount)
	END
	-- Nếu đã có món này (cập nhật số lượng)
	ELSE
	BEGIN
		UPDATE BillInfo SET Amount = @foodAmount + @amount WHERE ID = @isExistBillInfo
	END
END
GO

-- Thủ tục xóa món ăn của hóa đơn
CREATE PROC USP_DeleteBillInfo
@tableID INT, @foodID INT, @amount INT
AS
BEGIN
	-- Tìm hóa đơn chưa thanh toán của bàn xóa món ăn
	DECLARE @billID INT = -1
	SELECT @billID = ID FROM dbo.Bill WHERE TableID = @tableID AND Status = 0

	-- Nếu chưa có hóa đơn thì không cần xóa
	IF(@billID = -1)
		RETURN

	DECLARE @isExistBillInfo INT = -1
	DECLARE @foodAmount INT = 0

	SELECT @isExistBillInfo = ID, @foodAmount = Amount
	FROM BillInfo
	WHERE BillID = @billID AND FoodID = @foodID

	-- Nếu món ăn chưa được thêm trước đó thì không xóa
	IF(@isExistBillInfo = -1)
		RETURN
	-- Nếu đã có món này (cập nhật số lượng)
	ELSE
	BEGIN
		DECLARE @newAmount INT = @foodAmount - @amount
		IF(@newAmount > 0)
			UPDATE BillInfo SET Amount = @newAmount WHERE ID = @isExistBillInfo
		ELSE
			DELETE FROM BillInfo WHERE ID = @isExistBillInfo
	END
END
GO

-- Thủ tục xóa toàn bộ thông tin của hóa đơn
CREATE PROC USP_DeleteUnpaidBill
@tableID INT
AS
BEGIN
	-- Tìm hóa đơn chưa thanh toán của bàn
	DECLARE @billID INT = -1
	SELECT @billID = ID FROM dbo.Bill WHERE TableID = @tableID AND Status = 0
	
	-- Nếu chưa có hóa đơn thì không cần xóa
	IF(@billID = -1)
		RETURN

	-- Xóa tất cả thông tin hóa đơn của hóa đơn đó
	DELETE FROM BillInfo WHERE BillID =	@billID
	-- Hóa đơn sẽ tự động được xóa bởi TRIGGER do không còn thông tin hóa đơn
	-- Hoặc nếu kỹ hơn
	DELETE FROM Bill WHERE ID =	@billID
	
	UPDATE TableFood SET StatusID = dbo.UF_GetTableStatusID(N'Trống') WHERE ID = @tableID

END
GO

-- Cập nhật sự tồn tại hóa đơn khi xóa một hoặc nhiều thông tin hóa đơn
CREATE TRIGGER UTG_DeleteBillInfo
ON dbo.BillInfo FOR DELETE
AS
BEGIN
	DECLARE @billID INT

	DECLARE MY_CURSOR CURSOR 
	LOCAL STATIC READ_ONLY FORWARD_ONLY
	FOR 
	SELECT BillID
	FROM Deleted

	DECLARE @count INT

	OPEN MY_CURSOR
	FETCH NEXT FROM MY_CURSOR INTO @billID
	WHILE @@FETCH_STATUS = 0
	BEGIN
		-- Kiểm tra hóa đơn còn thông tin nào hay không
		SELECT @count = COUNT(*) FROM BillInfo WHERE BillID = @billID

		-- Nếu không còn thông tin về hóa đơn này => Xóa hóa đơn này đi
		IF(@count = 0)
			DELETE FROM Bill WHERE ID = @billID

		FETCH NEXT FROM MY_CURSOR INTO @billID
	END
	CLOSE MY_CURSOR
	DEALLOCATE MY_CURSOR
END
GO

-- Cập nhật trạng thái bàn khi thêm hóa đơn mới => Bàn có người
CREATE TRIGGER UTG_InsertBill
ON dbo.Bill FOR INSERT
AS
BEGIN
	DECLARE @tableID INT
	SELECT @tableID = TableID FROM Inserted
	UPDATE TableFood SET StatusID = dbo.UF_GetTableStatusID(N'Có người') WHERE ID = @tableID
END
GO


-- Cập nhật trạng thái bàn khi thanh toán hóa đơn
CREATE TRIGGER UTG_UpdateBill
ON dbo.Bill FOR UPDATE
AS
BEGIN
	DECLARE @status INT
	SELECT @status = Status FROM Inserted

	-- Nếu là đang thanh toán hóa đơn
	IF(@status = 1)
	BEGIN
		DECLARE @tableID INT = -1
		SELECT @tableID = TableID FROM Inserted
		UPDATE TableFood SET StatusID = dbo.UF_GetTableStatusID(N'Trống') WHERE ID = @tableID
	END
END
GO

-- Cập nhật trạng thái bàn khi xóa hóa đơn (chỉ trường hợp chưa thanh toán) => Bàn trống
CREATE TRIGGER UTG_DeleteBill
ON dbo.Bill FOR DELETE
AS
BEGIN
	-- Kiểm tra trạng thái hóa đơn có thanh toán hay chưa?
	DECLARE @status INT
	SELECT @status = Status FROM Deleted
	
	-- Chỉ khi xóa hóa đơn chưa thanh toán => Bàn trống
	IF(@status = 0)
	BEGIN
		DECLARE @tableID INT = -1
		SELECT @tableID = TableID FROM Deleted
		UPDATE TableFood SET StatusID = dbo.UF_GetTableStatusID(N'Trống') WHERE ID = @tableID
	END
END
GO

-- Chuyển bàn
CREATE PROC USP_SwitchTable
@tableID1 INT, @tableID2 INT
AS
BEGIN
	DECLARE @billID1 INT = -1
	DECLARE @billID2 INT = -1

	-- Lấy ra 2 hóa đơn chưa thanh toán tương ứng của 2 bàn
	SELECT @billID1 = ID FROM Bill WHERE TableID = @tableID1 AND Status = 0
	SELECT @billID2 = ID FROM Bill WHERE TableID = @tableID2 AND Status = 0

	-- Nếu bàn 1 có hóa đơn chưa thanh toán => Chuyển sang bàn 2
	IF (@billID1 = -1)
		UPDATE TableFood SET StatusID = dbo.UF_GetTableStatusID(N'Trống') WHERE ID = @tableID2
	ELSE
	BEGIN
		UPDATE Bill SET TableID = @tableID2 WHERE ID = @billID1
		UPDATE TableFood SET StatusID = dbo.UF_GetTableStatusID(N'Có người') WHERE ID = @tableID2
	END
		
	-- Nếu bàn 2 có hóa đơn chưa thanh toán => Chuyển sang bàn 1
	IF (@billID2 = -1)
		UPDATE TableFood SET StatusID = dbo.UF_GetTableStatusID(N'Trống') WHERE ID = @tableID1
	ELSE
	BEGIN
		UPDATE Bill SET TableID = @tableID1 WHERE ID = @billID2
		UPDATE TableFood SET StatusID = dbo.UF_GetTableStatusID(N'Có người') WHERE ID = @tableID1
	END
END
GO

-- Gộp bàn
CREATE PROC USP_MergeTable
@tableID1 INT, @tableID2 INT
AS
BEGIN
	DECLARE @billID1 INT = -1
	DECLARE @billID2 INT = -1

	-- Lấy ra 2 hóa đơn chưa thanh toán tương ứng của 2 bàn
	SELECT @billID1 = ID FROM Bill WHERE TableID = @tableID1 AND Status = 0
	SELECT @billID2 = ID FROM Bill WHERE TableID = @tableID2 AND Status = 0

	-- Nếu bàn 1 không có hóa đơn chưa thanh toán => Không cần gộp
	IF(@billID1 = -1)
	BEGIN
		RETURN
	END

	-- Nếu bàn 2 không có hóa đơn chưa thanh toán => Biến hóa đơn của bàn 1 thành của bàn 2
	IF (@billID2 = -1)
	BEGIN
		UPDATE Bill SET TableID = @tableID2 WHERE ID = @billID1
		UPDATE TableFood SET StatusID = dbo.UF_GetTableStatusID(N'Trống') WHERE ID = @tableID1
		UPDATE TableFood SET StatusID = dbo.UF_GetTableStatusID(N'Có người') WHERE ID = @tableID2
		RETURN
	END

	DECLARE MY_CURSOR CURSOR 
	LOCAL STATIC READ_ONLY FORWARD_ONLY
	FOR 
	SELECT ID, FoodID, Amount
	FROM BillInfo
	WHERE BillID = @billID1

	DECLARE @billInfoID INT
	DECLARE @foodID INT
	DECLARE @amount INT

	OPEN MY_CURSOR
	FETCH NEXT FROM MY_CURSOR INTO @billInfoID, @foodID, @amount
	WHILE @@FETCH_STATUS = 0
	BEGIN
		DECLARE @isExistBillInfo INT = -1
		DECLARE @foodAmount INT = -1

		SELECT @isExistBillInfo = ID, @foodAmount = Amount
		FROM BillInfo
		WHERE BillID = @billID2 AND FoodID = @foodID

		-- Nếu món ăn chưa được thêm vào hóa đơn kia
		IF(@isExistBillInfo = -1)
		BEGIN
			-- Cập nhật mã hóa đơn
			UPDATE dbo.BillInfo SET BillID = @billID2 WHERE ID = @billInfoID
		END
		-- Nếu đã có món này trong hóa đơn kia 
		-- => Cập nhật số lượng của thông tin cũ hóa đơn 2 và xóa thông tin tương ứng trong hóa đơn 1 đi
		ELSE
		BEGIN
			DECLARE @newAmount INT = @foodAmount + @amount
			UPDATE BillInfo SET Amount = @newAmount WHERE ID = @isExistBillInfo
			DELETE BillInfo WHERE ID = @billInfoID
		END
		FETCH NEXT FROM MY_CURSOR INTO @billInfoID, @foodID, @amount
	END
	CLOSE MY_CURSOR
	DEALLOCATE MY_CURSOR

	-- Cập nhật trạng thái bàn trước khi bị gộp vào là trống
	UPDATE TableFood SET StatusID = dbo.UF_GetTableStatusID(N'Trống') WHERE ID = @tableID1
	-- Xóa hóa đơn
	DELETE FROM Bill WHERE ID = @billID1
END
GO

-- Lấy danh sách hóa đơn đã thanh toán theo khoảng thời gian
CREATE PROC USP_GetListBillByDate
@checkIn DATE, @checkOut DATE
AS
BEGIN
	SELECT t.Name AS [Tên bàn], b.TotalPrice AS [Tổng tiền], b.Discount AS [Giảm giá], b.DateCheckIn AS [Ngày vào], b.DateCheckOut AS [Ngày ra]
	FROM dbo.Bill AS b INNER JOIN dbo.TableFood AS t ON b.TableID = t.ID
	WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.Status = 1
END
GO

-- Cập nhật thông tin tài khoản
CREATE PROC USP_UpdateAccount
@username NVARCHAR(100), @displayName NVARCHAR(100), @password NVARCHAR(1000), @newPass NVARCHAR(1000)
AS
BEGIN
	DECLARE @isRightPass INT = -1
	SELECT @isRightPass = COUNT(*) FROM Account WHERE Username = @username AND Password = @password

	IF(@isRightPass = 0)
		RETURN

	UPDATE Account SET DisplayName = @displayName, Password = @newPass WHERE Username = @username

END
GO

-- Hàm chuyển ký tự có dấu về không dấu
CREATE FUNCTION ConvertToUnsign
( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) 
AS 
BEGIN
	IF @strInput IS NULL 
	RETURN @strInput 
	IF @strInput = '' 
	RETURN @strInput 
	
	DECLARE @RT NVARCHAR(4000) 
	DECLARE @SIGN_CHARS NCHAR(136) 
	DECLARE @UNSIGN_CHARS NCHAR (136) 
	SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) 
	SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' 
	
	DECLARE @COUNTER int 
	DECLARE @COUNTER1 int 
	SET @COUNTER = 1 
	WHILE (@COUNTER <=LEN(@strInput)) 
	BEGIN 
		SET @COUNTER1 = 1 
		WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) 
		BEGIN 
			IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) 
			BEGIN 
				IF @COUNTER=1 
					SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) 
				ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) 
				BREAK 
			END 
			SET @COUNTER1 = @COUNTER1 +1 
		END 
		SET @COUNTER = @COUNTER +1 
	END 
	SET @strInput = replace(@strInput,' ','-') 
	RETURN @strInput 
END
GO

-- Phân trang hóa đơn (Lấy ra các bản ghi theo trang tương ứng với số dòng trên một trang cho trước)
CREATE PROC USP_GetListBillByDateAndPage
@checkIn DATE, @checkOut DATE, @linesInAPage INT, @pageNumber INT
AS 
BEGIN
	-- Số dòng của các trang trước không lấy
	DECLARE @exceptRows INT = (@pageNumber - 1) * @linesInAPage
	
	;WITH BillShow 
	AS (SELECT b.ID, t.Name AS [Tên bàn], b.TotalPrice AS [Tổng tiền], b.Discount AS [Giảm giá], b.DateCheckIn AS [Ngày vào], b.DateCheckOut AS [Ngày ra]
	FROM dbo.Bill AS b INNER JOIN dbo.TableFood AS t ON b.TableID = t.ID
	WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.Status = 1)
	
	SELECT TOP (@linesInAPage) [Tên bàn], [Tổng tiền], [Giảm giá], [Ngày vào], [Ngày ra]
	FROM BillShow 
	WHERE ID NOT IN (SELECT TOP (@exceptRows) ID FROM BillShow)
END
GO

-- Đếm số bản ghi hóa đơn
CREATE PROC USP_CountBillRecordByDate
@checkIn date, @checkOut date
AS 
BEGIN
	SELECT COUNT(*)
	FROM dbo.Bill AS b INNER JOIN dbo.TableFood AS t ON b.TableID = t.ID
	WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.Status = 1
END
GO

-- Lấy danh sách hóa đơn để báo cáo
CREATE PROC USP_GetListBillForReport
@checkIn DATE, @checkOut DATE
AS
BEGIN
	SELECT t.Name, b.TotalPrice, b.Discount, b.DateCheckIn, b.DateCheckOut
	FROM dbo.Bill AS b INNER JOIN dbo.TableFood AS t ON b.TableID = t.ID
	WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.Status = 1
END
GO

-- Xuất hóa đơn
CREATE PROC USP_GetBillForReport
@tableID INT
AS
BEGIN
	SELECT f.Name, bi.Amount, f.Price, f.Price * bi.Amount AS [TotalPrice]
	FROM BillInfo AS bi 
	INNER JOIN Bill AS b ON bi.BillID = b.ID 
	INNER JOIN Food AS f ON bi.FoodID = f.ID 
	WHERE b.TableID = @tableID AND b.Status = 0
END
GO

---------------------------------------------------
--SELECT * FROM dbo.AccountType
--SELECT * FROM dbo.Account
--SELECT * FROM dbo.TableStatus
--SELECT * FROM dbo.TableFood
--SELECT * FROM dbo.FoodCategory
--SELECT * FROM dbo.Food
--SELECT * FROM dbo.Bill
--SELECT * FROM dbo.BillInfo
--GO

--SELECT * FROM Food WHERE dbo.ConvertToUnsign(Name) LIKE dbo.ConvertToUnsign(N'%càm%')
--GO

--DELETE FROM dbo.BillInfo
--DELETE FROM dbo.Bill
--DELETE FROM dbo.Food
--DELETE FROM dbo.FoodCategory
--DELETE FROM dbo.TableFood
--DELETE FROM dbo.TableStatus
--DELETE FROM dbo.Account
--DELETE FROM dbo.AccountType
--GO

-- DROP DATABASE Data