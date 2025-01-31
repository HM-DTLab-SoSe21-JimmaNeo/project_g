﻿using System;
using SEIIApp.Shared.DomainTdo;

namespace SEIIApp.Server.Domain
{

    // Represents an video element for chapters.
    public class VideoDefinition: ChapterElementDefinition
    {

        public String Description { get; set; }

        public Uri VideoUri { get; set; }

    }
}
