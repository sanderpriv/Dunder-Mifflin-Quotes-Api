package no.dunder.mifflin.dundermifflinquotes.db

import no.dunder.mifflin.dundermifflinquotes.domain.dto.Quote

interface IResourceService {
    fun getQuotesFromCsvFile(): List<Quote>
}