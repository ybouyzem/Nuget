# .NET JSON/XML Lab

## Overview
A C# console application demonstrating JSON and XML handling using Newtonsoft.Json.

## Tasks

### v1.0 — Task 1: XML Reader
- Manual `users.json` file created
- `users.xml` created as XML theory example
- `XmlDocument` used to read and parse XML entries

### v2.0 — Task 2: Add Entries to JSON
- New entries appended to `users.json` at runtime using `JArray`
- File updated and saved back to disk

### v3.0 — Task 3: Deserialize All Users
- Full `users.json` deserialized into `List<User>` using Newtonsoft.Json
- Loop iterates over all entries and prints to console

### v4.0 — Task 4: Inheritance + user_types.json
- `User` base class extended with `AdminUser`, `RegularUser`, `Moderator`
- `user_types.json` created with `UserType` discriminator field
- JSON parsed with `JArray`, each object cast to the correct subtype
- All entries printed with type-specific field
