# csa-demo-container-bcl Development

## Overview

This repository contains the source code for the **demo-container-bcl** container application owned by **csa**.

## Repository Structure

```
├── .github/workflows/    # CI/CD pipeline (Docker build & push)
├── src/                  # Application source code
├── Dockerfile            # Container image definition
└── docs/                 # Documentation (this folder)
```

## Related Repositories

| Repository | Purpose |
|------------|---------|
| [csa-demo-container-bcl-dev](https://github.com/mjay-bs-test/csa-demo-container-bcl-dev) | Application source code (this repo) |
| [csa-demo-container-bcl-deploy](https://github.com/mjay-bs-test/csa-demo-container-bcl-deploy) | GitOps deployment configuration |

## Getting Started

### Local Development

1. Clone this repository
2. Build the Docker image:
   ```bash
   docker build -t demo-container-bcl .
   ```
3. Run locally:
   ```bash
   docker run -p 8080:80 demo-container-bcl
   ```

### CI/CD Pipeline

The GitHub Actions workflow automatically:
1. Builds the Docker image on push to `main`
2. Pushes to GitHub Container Registry (ghcr.io)
3. Tags with commit SHA and `latest`

## Support

For issues or questions, contact the **csa** team.
