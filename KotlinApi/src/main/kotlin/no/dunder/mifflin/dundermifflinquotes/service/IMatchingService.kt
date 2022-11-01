package no.dunder.mifflin.dundermifflinquotes.service

import no.dunder.mifflin.dundermifflinquotes.domain.dto.Quote

interface IMatchingService {

    fun getMatchesOfRedditCommentsAndQuotes(originalComments: List<String>, originalQuotes: List<Quote>): Map<String, Int>

}