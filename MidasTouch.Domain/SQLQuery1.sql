delete from Users;
delete from Identities;
delete from Portfolios;
delete from Shares;
delete from Identities;
delete from Names;

select First,Last,NameId,Identities.Email from Names left outer join Identities on Identities.Id=dbo.Names.Id;
select Users.IdentityId,Users.PortfolioId,Users.AccountBalance from Users left outer join Portfolios on Portfolios.Id=Users.PortfolioId;
select * from Shares;


