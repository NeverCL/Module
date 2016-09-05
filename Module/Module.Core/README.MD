## 说明

Nuget:Install-Package Module.Core -version 1.0

## 功能

- 4种BaseDbContext
- 各种AuditEntity

## 类

- BaseDbContext
	- 构造函数
	- 审计接口记录
	- 去除级联删除
	- 日志(todo)

- ZeroBaseDbContext
	- 支持IdentityEF

- BaseEntity : BaseEntity<T> : IEntity<T>
	- Id

- CreationAuditedEntity : CreationAuditedEntity<T> : BaseEntity<T>, ICreatorEntity
	- CreatorId
	- CreateTime

- AuditedEntity : AuditedEntity<T> : CreationAuditedEntity<T>, IModificationAudited
	- LastModificationTime?
	- LastModifierUserId

- FullAuditedEntity : FullAuditedEntity<T> : AuditedEntity<T>, IDeletionAudited
	- DeletionTime?
	- DeleterUserId

- IDeletionAudited : ISoftDelete
	- IsDeleted

- IActived
	- IsActived

