package no.dunder.mifflin.dundermifflinquotes.service

import no.dunder.mifflin.dundermifflinquotes.domain.dto.Quote

interface IResourceService {
    fun getQuotesFromCsvFile(): List<Quote>
}