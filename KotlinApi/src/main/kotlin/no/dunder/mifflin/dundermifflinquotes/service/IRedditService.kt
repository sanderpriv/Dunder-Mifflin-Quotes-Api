package no.dunder.mifflin.dundermifflinquotes.service

interface IRedditService {

    fun getCommentsFromLast24Hours(): List<String>

}