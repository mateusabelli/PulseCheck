# 📈 PulseCheck

A tiny desktop resource monitor designed to practice Test-Driven Development (TDD) and CI/CD integration.

## Roadmap

📍 Phase 1: Foundation & Core Logic (CLI)

Goal: Build the "brain" of the app without a GUI to ensure it is 100% testable.

- [ ] Project Setup: Initialize git, choose a license, and set up the directory structure.
- [ ] Data Models: Define a ResourceSnapshot object to hold CPU/RAM data.
- [ ] The "Monitor" Engine: - Write logic to fetch system stats (using psutil for Python or System.Diagnostics for C#).
- [ ] Unit Test: Mock the system calls to ensure the engine handles "fake" data correctly.
- [ ] Alert Logic: - Create a function that determines status (Healthy vs. Critical) based on thresholds.
- [ ] Unit Test: Validate that Usage>Threshold consistently triggers the correct state.

🧪 Phase 2: CI/CD Integration

Goal: Automate the boring stuff early.

- [ ] GitHub Actions Setup: Create .github/workflows/test.yml.
- [ ] Automated Testing: Ensure every git push triggers the unit test suite.
- [ ] Linting: Add a step to check for code formatting (e.g., Flake8 or Prettier).
- [ ] Badge: Add a "Build Passing" badge to the README.md.

🖼️ Phase 3: Desktop UI Development

Goal: Wrap the logic in a functional, clean interface.

- [ ] Basic Layout: Create a simple window showing two progress bars (CPU/RAM).
- [ ] The "Visual Alert" System: Implement a UI change (e.g., background turns red) when the Logic Engine signals a "Critical" state.
- [ ] User Settings: Allow users to set their own threshold (e.g., notify at 80% vs 90%).

📦 Phase 4: Distribution & Release

Goal: Make the app usable by someone who doesn't have your dev environment.

- [ ] Logging Feature: Implement a "Save Logs" button that exports history to a .csv.
- [ ] Build Pipeline: Update GitHub Actions to "Build and Release" (generate an .exe or .app file).
- [ ] Release Tagging: Set up a workflow that creates a GitHub Release whenever a new version tag (e.g., v1.0.0) is pushed.

🌟 Bonus / Future Ideas

- [ ] Dark Mode: Support system-wide theme switching.
- [ ] History Graph: Use a charting library to show usage over the last 60 seconds.
- [ ] Performance Optimization: Ensure the app uses <1% of the CPU it is actually monitoring.
