# Secrets

This file contains the command needed to set all the secrets required to run IoT.Architectures.Api.
Execute the commands in the root of the repository.

### Command

Execute this command to set a secret.
Set the key to the name of the secret and the value to the value of the secret.
Also set the project to the project where the secret is used.

```bash
dotnet user-secrets set "KEY" "SECRET" --project "../src/api/IoT-Architectures.Api/IoT-Architectures.Api.csproj"
```

```bash
dotnet user-secrets remove "KEY" --project "../src/api/IoT-Architectures.Api/IoT-Architectures.Api.csproj"
```

### Keys

| Key                |
|--------------------|
| Mongodb:Username   |
| Mongodb:Password   |
