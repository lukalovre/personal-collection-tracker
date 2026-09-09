# Agent Guidance

## Project shape

- This is a .NET 10 Avalonia desktop application. The main project is `CollectionTracker.csproj`.
- Startup is configured in `Program.cs`; `App.axaml.cs` creates the main window and its view model.
- `Views/` contains Avalonia views, `ViewModels/` contains ReactiveUI view models, and `Models/` contains collection/domain models.
- `MainWindowViewModel` composes the feature view models directly. `ItemViewModel<TItem, TGridItem, TEventItem>` owns shared collection behavior such as loading, filtering, CRUD commands, selection, images, and external lookups.
- Persistence is behind `Repositories/IDatasource.cs`; `Repositories/TsvDatasource.cs` stores TSV data. External services are represented by `IExternal<T>` implementations under `Repositories/External` and `Repositories/ItemExternals`.
- Runtime data and API keys are outside the source tree and are resolved through `Repositories/Paths.cs` and `Settings.json`. Do not assume repository-local fixture data exists.

## Working conventions

- Preserve the existing paired naming: `SomethingView.axaml`/`.axaml.cs` and `SomethingViewModel`.
- Use ReactiveUI patterns already present in the project for observable properties and commands.
- Keep new persistence and external-service behavior behind the existing interfaces rather than constructing provider-specific logic in views.
- Preserve public APIs and existing spellings unless a rename is explicitly required; several misspelled names are already referenced (`FileRepsitory`, `BookExtetrnal`, `ComicExtetrnal`, and `GameExtetrnal`).
- Follow the surrounding namespace and formatting style in the file being changed. Avoid unrelated cleanup.
- Treat `bin/` and `obj/` output as generated artifacts; current source targets `net10.0` and Avalonia 12.

## Validation

- Build the application with `dotnet build CollectionTracker.csproj` after code changes.
- Use `dotnet watch run --project CollectionTracker.csproj` for an interactive UI check when needed.
- `dotnet publish CollectionTracker.csproj` validates publishing; there is currently no test project or configured automated test command.
- Changes involving data should account for TSV files, image paths, settings, and API-key files outside the repository. Avoid destructive changes to user data.

## Useful references

- [Project overview](README.md)
- [Project configuration and package versions](CollectionTracker.csproj)
- [VS Code build/run tasks](.vscode/tasks.json)
