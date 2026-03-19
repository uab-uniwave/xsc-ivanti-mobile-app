# HeatReplayClient

Environment variables:

- HEAT_BASE_URL = https://stg-heat20254.synergy.lt
- HEAT_REST_API_KEY = your rest_api_key value
- HEAT_TZOFFSET = -120
- HEAT_OUTPUT_DIR = responses

Optional overrides:
- HEAT_WORKSPACE_OBJECT_ID = Incident#
- HEAT_WORKSPACE_LAYOUT = IncidentLayout.ResponsiveAnalyst
- HEAT_FORM_LAYOUT = IncidentLayout.ResponsiveAnalyst
- HEAT_FORM_VIEW = responsive.analyst.new

## Run

dotnet run

Responses are saved as JSON files in the output directory.
