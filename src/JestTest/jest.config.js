module.exports = {
    "roots": [
        "<rootDir>/src/ts"
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
            "pageTitle": "Test Report"
           }
		]
    ]
}