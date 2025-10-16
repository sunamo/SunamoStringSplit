# NuGet Alternatives to SunamoStringSplit

This document lists popular NuGet packages that provide similar functionality to SunamoStringSplit.

## Overview

String splitting utilities

## Alternative Packages

### System.String.Split
- **NuGet**: System.Runtime
- **Purpose**: Built-in splitting
- **Key Features**: Split by separators, options

### System.MemoryExtensions
- **NuGet**: System.Memory
- **Purpose**: Span-based splitting
- **Key Features**: High performance, zero-allocation splitting

### MoreLINQ
- **NuGet**: morelinq
- **Purpose**: Advanced splitting
- **Key Features**: Split into chunks

## Comparison Notes

String.Split() is standard. Span<char>.Split() for performance-critical code.

## Choosing an Alternative

Consider these alternatives based on your specific needs:
- **System.String.Split**: Built-in splitting
- **System.MemoryExtensions**: Span-based splitting
- **MoreLINQ**: Advanced splitting
