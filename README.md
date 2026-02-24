# 📈 PulseCheck

Tiny experiment of a desktop app that monitors computer resource usage

> [!IMPORTANT]
> Work in progress...

## Roadmap

### Phase 1: Foundation & Core Logic (CLI)

- [x] Project Setup: Initialize git, choose a license, and set up the directory structure.
- [x] Data Models: Define a ResourceSnapshot object to hold CPU/RAM data.
- [x] The "Monitor" Engine: - Write logic to fetch system stats.
- [x] Unit Test: Mock the system calls to ensure the engine handles "fake" data correctly.
- [ ] Alert Logic: - Create a function that determines status (Healthy vs. Critical) based on thresholds.
- [ ] Unit Test: Validate that Usage>Threshold consistently triggers the correct state.

### Phase 2: CI/CD Integration

- [ ] GitHub Actions Setup: Create .github/workflows/test.yml.
- [ ] Automated Testing: Ensure every git push triggers the unit test suite.
- [ ] Linting: Add a step to check for code formatting.
- [ ] Badge: Add a "Build Passing" badge to the README.md.

### Phase 3: Desktop UI Development

- [ ] Basic Layout: Create a simple window showing two progress bars (CPU/RAM).
- [ ] The "Visual Alert" System: Implement a UI change (e.g., background turns red) when the Logic Engine signals a "
  Critical" state.
- [ ] User Settings: Allow users to set their own threshold (e.g., notify at 80% vs 90%).

### Phase 4: Distribution & Release

- [ ] Logging Feature: Implement a "Save Logs" button that exports history to a .csv.
- [ ] Build Pipeline: Update GitHub Actions to "Build and Release".
- [ ] Release Tagging: Set up a workflow that creates a GitHub Release whenever a new version tag (e.g., v1.0.0) is
  pushed.

### Bonus / Future Ideas

- [ ] Dark Mode: Support system-wide theme switching.
- [ ] History Graph: Use a charting library to show usage over the last 60 seconds.
- [ ] Performance Optimization: Ensure the app uses <1% of the CPU it is actually monitoring.
