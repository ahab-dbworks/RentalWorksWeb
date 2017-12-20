declare var templates: {
    main: (d?: any) => string,
    home: (d?: any) => string,
    doc: (d?: any) => string,
    editor: {
        main: (d?: any) => string;
        filenav: (d?: any) => string;
        filenav_mode_1: (d?: any) => string;
        filenav_mode_2: (d?: any) => string;
        module: (d?: any) => string;
        scan: (d?: any) => string;
        scan_folders_files: (d?: any) => string;
        designer: (d?: any) => string;
        recents: (d?: any) => string;
        settings: (d?: any) => string;
        options_context: (d?: any) => string;
    },
    sandbox: (d?: any) => string,
    error: (d?: any) => string
}