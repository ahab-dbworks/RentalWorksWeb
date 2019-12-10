module.exports = {
    "roots": [
        "<rootDir>/src/ts/rentalworksapi"
    ],
    "transform": {
        "^.+\\.tsx?$": "ts-jest"
    },
    "testRegex": "(/__tests__/.*|(\\.|/)(test|spec))\\.tsx?$",
    "moduleFileExtensions": [
        "ts",
        "tsx",
        "js",
        "jsx",
        "json",
        "node"
    ],
    "reporters": [
        "default",
        [
		"./node_modules/jest-html-reporter", {
            "pageTitle": "RentalWorks Api Test Report",
			"outputPath": "test-report-rentalworksapi.html",
			includeFailureMsg : true,
			includeConsoleLog : true,
			logo: "rwwlogo.png"
           }
		]
    ]
}