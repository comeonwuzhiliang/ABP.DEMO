# 初始化db结构和db的基本数据
cd src\Acme.BookStore.DbMigrator

echo '----------------------------- start init db---------------------'
dotnet run
echo '------------------------------end init db-----------------'

# 安装lib库
cd ../
cd Acme.BookStore.HttpApi.Host
abp install-libs
