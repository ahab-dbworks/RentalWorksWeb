module.exports = {
    "roots": [
        "<rootDir>/src/ts/rentalworksweb"
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
    "setupFilesAfterEnv": ["expect-puppeteer"],
    "preset": "jest-puppeteer"
}