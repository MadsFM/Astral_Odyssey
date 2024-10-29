#!/bin/bash
dotnet ef dbcontext scaffold \
  "Server=localhost;Database=Astral;User Id=Odyssey;Password=AstralPass;" \
  Npgsql.EntityFrameworkCore.PostgreSQL \
  --output-dir Models \
  --context-dir . \
  --context MyDbContext  \
  --no-onconfiguring \
  --data-annotations \
  --force