#!/bin/bash

dotnet ef dbcontext script \
  --project . \
  --startup-project ../Api \
  --context AppDbContext \
  --output ddl.sql
